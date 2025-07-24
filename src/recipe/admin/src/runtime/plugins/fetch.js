import { defineNuxtPlugin, useRuntimeConfig } from "#app";
import { ofetch } from "ofetch";

export default defineNuxtPlugin({
  name: "fetch-manager",
  enforce: "pre",
  setup(nuxtApp) {
    const interceptors = createInterceptors();
    const { public: { composables } } = useRuntimeConfig();

    globalThis.$fetch = ofetch.create({
      async onRequest(context) {
        // filters out `/_nuxt` calls and any other non api calls
        if(context.options.baseURL !== composables.useDataFetcher.baseURL) { return; }

        await interceptors.onRequest(context, nuxtApp);
      },
      async onResponse(context) {
        // filters out `/_nuxt` calls and any other non api calls
        if(context.options.baseURL !== composables.useDataFetcher.baseURL) { return; }

        await interceptors.onResponse(context, nuxtApp);
      }
    });

    // Add to nuxtApp for access from plugins
    nuxtApp.provide("fetchInterceptors", interceptors);
  }
});

function createInterceptors() {
  const interceptorMap = new Map();

  let interceptors = null;
  let sorted = false;

  function register(name,
    {
      onRequest = () => { },
      onResponse = () => { },
      priority = 100
    } = { }
  ) {
    interceptorMap.set(name, { onRequest, onResponse, priority });
    sorted = false;
  }

  function ensureSorted() {
    if(sorted && interceptors) { return; }

    interceptors = Array.from(interceptorMap.entries())
      .sort(([, a], [, b]) => a.priority - b.priority)
      .map(([_, interceptor]) => interceptor);
    sorted = true;
  }

  async function onRequest(context, nuxtApp) {
    ensureSorted();

    for(const interceptor of interceptors) {
      await interceptor.onRequest(context, nuxtApp);
    }
  }

  async function onResponse(context, nuxtApp) {
    ensureSorted();

    for(const interceptor of interceptors) {
      await interceptor.onResponse(context, nuxtApp);
    }
  }

  return {
    register,
    onRequest,
    onResponse
  };
};
