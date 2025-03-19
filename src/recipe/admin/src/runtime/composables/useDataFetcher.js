import { useComposableResolver } from "#imports";

export default function() {
  const composableResolver = useComposableResolver();

  function shouldLoad(dataType) {
    return dataType === "Remote" || dataType === "Computed";
  }

  function get(data) {
    return data?.type === "Inline" ? data.value : null;
  }

  async function fetch({ baseURL, data, routeParams, options }) {
    if(data?.type === "Remote") {
      const headers = data.headers
        ? await fetch({ baseURL, data: data.headers, routeParams, options })
        : { };

      return await $fetch(
        format(`${data.path}`, routeParams.slice(1)),
        {
          ...options ?? { },
          baseURL,
          headers
        }
      );
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

    if(data?.type === "Inline") {
      return data.value;
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
