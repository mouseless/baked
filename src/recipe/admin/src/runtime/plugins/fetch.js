import { defineNuxtPlugin } from "#app";
import useFetchInterceptors from "../composables/useFetchInterceptors";
import { ofetch } from "ofetch";

export default defineNuxtPlugin({
  name: "fetch-manager",
  enforce: "pre",
  setup(nuxtApp) {
    const { execute } = useFetchInterceptors();

    globalThis.$fetch = ofetch.create({
      async onRequest(context) {
        await execute(context, nuxtApp);
      }
    });

    // Add to nuxtApp for access from plugins
    nuxtApp.provide("fetchInterceptors", useFetchInterceptors());
  }
});