import { useRuntimeConfig } from "#app";
import { useComposableResolver, useUnref } from "#imports";

export default function() {
  const composableResolver = useComposableResolver();
  const unref = useUnref();
  const { public: { composables } } = useRuntimeConfig();

  function shouldLoad(dataType) {
    return dataType === "Remote" || dataType === "Computed" || dataType == "Composite";
  }

  function get({ data, injectedData }) {
    return data?.type === "Inline" ? inline({ data }) :
      data?.type === "Injected" ? injected({ data, injectedData }) :
        null;
  }

  async function fetch({ baseURL, data, injectedData }) {
    baseURL = baseURL || composables.useDataFetcher.baseURL;

    if(data?.type === "Composite") { return await composite({ baseURL, data, injectedData }); }
    if(data?.type === "Computed") { return await computed({ data }); }
    if(data?.type === "Injected") { return injected({ data, injectedData }); }
    if(data?.type === "Inline") { return inline({ data }); }
    if(data?.type === "Remote") { return await remote({ baseURL, data, injectedData }); }

    throw new Error(`${data?.type} is not a valid data type`);
  }

  async function composite({ baseURL, data, injectedData }) {
    const result = {};

    for(const part of data.parts) {
      Object.assign(
        result,
        unref.deepUnref(await fetch({ baseURL, data: part, injectedData }))
      );
    }

    return result;
  }

  async function computed({ data }) {
    const composable = (await composableResolver.resolve(data.composable)).default();

    if(composable.compute) {
      return composable.compute(...(data.args || []));
    }

    if(composable.computeAsync) {
      return await composable.computeAsync(...(data.args || []));
    }

    throw new Error("Data composable should have either `compute` or `computeAsync`");
  }

  function injected({ data, injectedData }) {
    const result = injectedData[data.key];

    if(!data.prop) { return result; }

    return result.value?.[data.prop];
  }

  function inline({ data }) {
    return data.value;
  }

  async function remote({ baseURL, data, injectedData }) {
    const headers = data.headers
      ? unref.deepUnref(await fetch({ baseURL, data: data.headers, injectedData }))
      : { };

    const query = data.query
      ? unref.deepUnref(await fetch({ baseURL, data: data.query, injectedData }))
      : { };

    const options = { baseURL, headers: headers, query: query };
    const retry = composables?.useDataFetcher?.retry ?? false;

    if(retry) {
      return await fetchWithRetry(data.path, options, retry);
    }

    return await $fetch(data.path, { ...options, retry: false });
  }

  async function fetchWithRetry(url, options,
    { maxRetry = Number.MAX_VALUE, delay = 200 } = { }
  ) {
    let lastError = null;
    for(let i = -1; i < maxRetry; i++) { // i starts at -1 to allow for the first attempt
      try {
        return await $fetch(url, { ...options, retry: false });
      } catch (error) {
        lastError = error;

        if(error.response) { // server is up and returned and error
          break;
        }

        await wait(delay);
      }
    }

    throw lastError;
  }

  function wait(ms) {
    return new Promise(resolve => setTimeout(resolve, ms));
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
