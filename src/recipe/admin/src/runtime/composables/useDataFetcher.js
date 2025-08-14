import { useRuntimeConfig } from "#app";
import { useComposableResolver, useUnref } from "#imports";

export default function() {
  const datas = {
    "Composite": Composite({ parentFetch: fetch, parentFetchParameters: fetchParameters }),
    "Computed": Computed(),
    "Injected": Injected(),
    "Inline": Inline(),
    "Remote": Remote({ parentFetch: fetch })
  };

  function shouldLoad(dataType) {
    return datas[dataType]?.fetch !== undefined;
  }

  function get({ data, injectedData }) {
    const type = data?.type;
    if(!type) { return null; }
    if(!datas[type]) { return null; }
    if(!datas[type].get) { return null; }

    return datas[type].get({ data, injectedData });
  }

  async function fetch({ data, injectedData }) {
    const fetcher = datas[data?.type];
    if(!fetcher) { throw new Error(`${data?.type} is not a valid data type`); }

    return fetcher.get
      ? fetcher.get({ data, injectedData })
      : await fetcher.fetch({ data, injectedData });
  }

  async function fetchParameters({ data, injectedData }) {
    const fetcher = datas[data?.type];

    return fetcher?.fetchParameters
      ? fetcher.fetchParameters({ data, injectedData })
      : [];
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
    fetchParameters,
    format
  };
}

function Composite({ parentFetch, parentFetchParameters }) {
  const unref = useUnref();

  async function fetch({ data, injectedData }) {
    const result = {};

    for(const part of data.parts) {
      Object.assign(
        result,
        unref.deepUnref(await parentFetch({ data: part, injectedData }))
      );
    }

    return result;
  }

  async function fetchParameters({ data, injectedData }) {
    const result = [];

    for(const part of data.parts) {
      result.push(
        ...unref.deepUnref(await parentFetchParameters({ data: part, injectedData }))
      );
    }

    return result;
  }

  return {
    fetch,
    fetchParameters
  };
}

function Computed() {
  const composableResolver = useComposableResolver();

  async function fetch({ data }) {
    const composable = (await composableResolver.resolve(data.composable)).default();

    if(composable.compute) {
      return composable.compute(...(data.args || []));
    }

    if(composable.computeAsync) {
      return await composable.computeAsync(...(data.args || []));
    }

    throw new Error("Data composable should have either `compute` or `computeAsync`");
  }

  function fetchParameters({ data }) {
    return data.args;
  }

  return {
    fetch,
    fetchParameters
  };
}

function Injected() {
  function get({ data, injectedData }) {
    const result = injectedData[data.key];

    if(!data.prop) { return result; }

    return result.value?.[data.prop];
  }

  return {
    get
  };
}

function Inline() {
  function get({ data }) {
    return data.value;
  }

  return {
    get
  };
}

function Remote({ parentFetch }) {
  const { public: { composables } } = useRuntimeConfig();
  const unref = useUnref();

  async function fetch({ data, injectedData }) {
    const baseURL = composables.useDataFetcher.baseURL;
    const headers = await fetchHeaders({ data, injectedData });
    const query = await fetchQuery({ data, injectedData });

    const options = { baseURL, headers: headers, query: query };
    if(data.options) {
      options.options = data.options;
    }
    const retry = composables?.useDataFetcher?.retry ?? false;

    if(retry) {
      return await fetchWithRetry(data.path, options, retry);
    }

    return await $fetch(data.path, { ...options, retry: false });
  }

  async function fetchHeaders({ data, injectedData }) {
    return data.headers
      ? unref.deepUnref(await parentFetch({ data: data.headers, injectedData }))
      : { };
  }

  async function fetchQuery({ data, injectedData }) {
    return data.query
      ? unref.deepUnref(await parentFetch({ data: data.query, injectedData }))
      : { };
  }

  async function fetchParameters(options) {
    return Object.values(await fetchQuery(options));
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

  return {
    fetch,
    fetchParameters
  };
}
