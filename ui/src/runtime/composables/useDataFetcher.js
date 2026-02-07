import { useRuntimeConfig } from "#app";
import { useComposableResolver, usePathBuilder, useUnref } from "#imports";

export default function() {
  const datas = {
    "Composite": Composite({ parentGet: get, parentFetch: fetch, parentFetchParameters: fetchParameters }),
    "Computed": Computed({ parentGet: get, parentFetch: fetch }),
    "Context": Context(),
    "Inline": Inline(),
    "Remote": Remote({ parentFetch: fetch })
  };

  function shouldLoad(data) {
    if(!data) { return false; }

    return data?.isAsync
      ? data.isAsync
      : datas[data.type]?.isAsync;
  }

  function get({ data, contextData }) {
    const type = data?.type;
    if(!type) { return null; }
    if(!datas[type]) { return null; }
    if(!datas[type].get) { return null; }
    if(shouldLoad(data)) { return null; }

    return datas[type].get({ data, contextData });
  }

  async function fetch({ data, contextData }) {
    const fetcher = datas[data?.type];
    if(!fetcher) { throw new Error(`${data?.type} is not a valid data type`); }

    return fetcher.get
      ? fetcher.get({ data, contextData })
      : await fetcher.fetch({ data, contextData });
  }

  async function fetchParameters({ data, contextData }) {
    const fetcher = datas[data?.type];

    return fetcher?.fetchParameters
      ? fetcher.fetchParameters({ data, contextData })
      : [];
  }

  return {
    shouldLoad,
    get,
    fetch,
    fetchParameters
  };
}

function Composite({ parentGet, parentFetch, parentFetchParameters }) {
  const unref = useUnref();

  function get({ data, contextData }) {
    const result = {};

    for(const part of data.parts) {
      Object.assign(
        result,
        unref.deepUnref(parentGet({ data: part, contextData }))
      );
    }

    return result;
  }

  async function fetch({ data, contextData }) {
    const result = {};

    for(const part of data.parts) {
      Object.assign(
        result,
        unref.deepUnref(await parentFetch({ data: part, contextData }))
      );
    }

    return result;
  }

  async function fetchParameters({ data, contextData }) {
    const result = [];

    for(const part of data.parts) {
      result.push(
        unref.deepUnref(await parentFetchParameters({ data: part, contextData }))
      );
    }

    return result;
  }

  return {
    isAsync: false,
    get,
    fetch,
    fetchParameters
  };
}

function Computed({ parentGet, parentFetch }) {
  const composableResolver = useComposableResolver();
  const unref = useUnref();

  function get({ data, contextData }) {
    const composable = composableResolver.resolve(data.composable).default();
    const options = data.options ? unref.deepUnref(parentGet({ data: data.options, contextData })) : { };

    return composable.compute(options);
  }

  async function fetch({ data, contextData }) {
    const composable = composableResolver.resolve(data.composable).default();
    const options = data.options ? unref.deepUnref(await parentFetch({ data: data.options, contextData })) : { };

    return await composable.compute(options);
  }

  async function fetchParameters({ data }) {
    return data.options ? unref.deepUnref(await parentFetch({ data: data.options })) : null;
  }

  return {
    isAsync: false,
    get,
    fetch,
    fetchParameters
  };
}

function Context() {
  const unref = useUnref();

  function get({ data, contextData }) {
    let result = contextData[data.key];

    if(!result) { return null; }

    result = unref.deepUnref(result);

    if(data.prop) {
      const path = data.prop.split(".");
      for(const prop of path) {
        result = result?.[prop];
      }
    }

    if(data.targetProp) {
      result = { [data.targetProp]: result };
    }

    return result;
  }

  return {
    isAsync: false,
    get
  };
}

function Inline() {
  function get({ data }) {
    return data.value;
  }

  return {
    isAsync: false,
    get
  };
}

function Remote({ parentFetch }) {
  const pathBuilder = usePathBuilder();
  const { public: { apiBaseURL, composables } } = useRuntimeConfig();
  const unref = useUnref();

  async function fetch({ data, contextData }) {
    const headers = await fetchHeaders({ data, contextData });
    const query = await fetchQuery({ data, contextData });
    const params = await fetchParams({ data, contextData });

    const path = pathBuilder.build(data.path, params);

    const options = { baseURL: apiBaseURL, headers: headers, query: query };
    if(data.attributes) {
      options.attributes = data.attributes;
    }
    const retry = composables?.useDataFetcher?.retry ?? false;

    if(retry) {
      return await fetchWithRetry(path, options, retry);
    }

    return await $fetch(path, { ...options, retry: false });
  }

  async function fetchHeaders({ data, contextData }) {
    return data.headers
      ? unref.deepUnref(await parentFetch({ data: data.headers, contextData }))
      : { };
  }

  async function fetchQuery({ data, contextData }) {
    return data.query
      ? unref.deepUnref(await parentFetch({ data: data.query, contextData }))
      : { };
  }

  async function fetchParams({ data, contextData }) {
    return data.params
      ? unref.deepUnref(await parentFetch({ data: data.params, contextData }))
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
    isAsync: true,
    fetch,
    fetchParameters
  };
}
