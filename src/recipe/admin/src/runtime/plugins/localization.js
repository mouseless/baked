import { defineNuxtPlugin, useRuntimeConfig } from "#app";
import { ofetch } from "ofetch";

export default defineNuxtPlugin({
  name: "auth",
  enforce: "pre",
  setup(nuxtApp) {
    const { public: { composables } } = useRuntimeConfig();

    globalThis.$fetch = ofetch.create({
      async onRequest({ options }) {
      // filters out `/_nuxt` calls and any other non api calls
        if(options.baseURL !== composables.useDataFetcher.baseURL) { return; }

        options.headers.set("Accept-Language", nuxtApp.$i18n.locale.value);
      }
    });
  }
});
