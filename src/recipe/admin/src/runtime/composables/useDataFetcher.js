import { useRuntimeConfig } from "#app";
import { useComposableResolver, useContext } from "#imports";
import { deepUnref } from "vue-deepunref";

export default function() {
  const composableResolver = useComposableResolver();
  const context = useContext();
  const { public: { composables } } = useRuntimeConfig();

  function shouldLoad(dataType) {
    return dataType === "Remote" || dataType === "Computed" || dataType == "Composite";
  }

  function get(data) {
    return data?.type === "Inline" ? data.value :
      data?.type === "Injected" ? context.injectedData() :
        null;
  }

  async function fetch({ baseURL, data, options, injectedData }) {
    baseURL = baseURL || composables.useDataFetcher.baseURL;

    if(data?.type === "Composite") {
      const result = {};

      for(const part of data.parts) {
        Object.assign(
          result,
          deepUnref(await fetch({ baseURL, data: part, options, injectedData }))
        );
      }

      return result;
    }

    if(data?.type === "Computed") {
      const composable = (await composableResolver.resolve(data.composable)).default();

      if(composable.compute) {
        return composable.compute(...(data.args || []));
      }

      if(composable.computeAsync) {
        return await composable.computeAsync(...(data.args || []));
      }

      throw new Error("Data composable should have either `compute` or `computeAsync`");
    }

    if(data?.type === "Injected") {
      return injectedData;
    }

    if(data?.type === "Inline") {
      return data.value;
    }

    if(data?.type === "Remote") {
      const headers = data.headers
        ? deepUnref(await fetch({ baseURL, data: data.headers, options, injectedData }))
        : { };

      const query = data.query
        ? deepUnref(await fetch({ baseURL, data: data.query, options, injectedData }))
        : { };

      return await $fetch(
        data.path,
        {
          ...options ?? { },
          baseURL,
          headers: headers,
          query: query
        }
      );
    }

    throw new Error(`${data?.type} is not a valid data type`);
  }

  function format(formatString, args) {
    // TODO: this path and format call is temporary, final design should handle
    // path variables using name, not index, e.g., /test/{0} -> /test/{id}
    return formatString.replace(/(\{\{\d\}\}|\{\d\})/g, part => {
      if(part.substring(0, 2) === "{{") { return part; } // escape

      const index = parseInt(part.match(/\d/)[0]);

      return args[index];
    });
  };

  return {
    shouldLoad,
    get,
    fetch,
    format
  };
}
