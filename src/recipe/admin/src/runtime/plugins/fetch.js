import { defineNuxtPlugin } from "#app";
import useFetchInterceptors from "../composables/useFetchInterceptors";
import { ofetch } from "ofetch";

export default defineNuxtPlugin({
  name: "fetch-manager",
  enforce: "pre",
  setup(nuxtApp) {
    const fetchInterceptors = useFetchInterceptors();
    const { public: { composables, options } } = useRuntimeConfig();

    globalThis.$fetch = ofetch.create({
      async onRequest(context) {
        // filters out `/_nuxt` calls and any other non api calls
        if(options.baseURL !== composables.useDataFetcher.baseURL) { return; }

        await fetchInterceptors.execute(context, nuxtApp);
      }
    });

    // Add to nuxtApp for access from plugins
    nuxtApp.provide("fetchInterceptors", fetchInterceptors);
  }
});