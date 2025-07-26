import { defineNuxtPlugin, useRuntimeConfig } from "#app";
import { ofetch } from "ofetch";

export default defineNuxtPlugin({
  name: "fetch-manager",
  enforce: "pre",
  setup(nuxtApp) {
    const fetch = ofetch.create();
    const interceptors = Interceptors();
    const optionsTemplate = { headers: { }, query: { }, options: { } };

    // register the actual fetch at the end of the interceptor pipeline
    interceptors.register("actual-fetch",
      async({ request, options }) => await fetch(request, options),
      Number.MAX_SAFE_INTEGER
    );

    // wrap $fetch using interceptors to allow around interception
    globalThis.$fetch = async(request, options) => {
      // not all requests have headers, query, options objects. this might
      // cause interceptors to fail. options template makes sure every
      // request has those objects to be set to an empty object.
      return await interceptors.execute({ request, options: { ...optionsTemplate, ...options } });
    };

    // Add to nuxtApp for access from plugins
    nuxtApp.provide("fetchInterceptors", interceptors);
  }
});

function Interceptors() {
  const { public: { composables } } = useRuntimeConfig();
  const interceptorMap = new Map();

  let interceptors = null;
  let sorted = false;

  function register(name, execute,
    priority = 0
  ) {
    interceptorMap.set(name, { execute, priority });
    sorted = false;
  }

  function ensureSorted() {
    if(sorted && interceptors) { return; }

    interceptors = Array.from(interceptorMap.entries())
      .sort(([, a], [, b]) => a.priority - b.priority)
      .map(([_, interceptor]) => interceptor);
    sorted = true;
  }

  async function execute(context) {
    ensureSorted();

    // doesn't intercept `/_nuxt` calls and any other non api calls
    if(context.options.baseURL !== composables.useDataFetcher.baseURL) {
      // directly executes last interceptor which is the actual fetch
      return await executeInner(context, interceptors.length - 1);
    }

    return await executeInner(context, 0);
  }

  async function executeInner(context, index) {
    if(index >= interceptors.length) { return; }

    const interceptor = interceptors[index];

    return await interceptor.execute(context, async() => {
      return await executeInner(context, index + 1);
    });
  }

  return {
    register,
    execute
  };
};
