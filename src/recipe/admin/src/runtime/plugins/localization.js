import { defineNuxtPlugin } from "#app";

export default defineNuxtPlugin({
  name: "localization",
  enforce: "pre",
  setup(nuxtApp) {
    const { $fetchInterceptors } = nuxtApp;

    $fetchInterceptors.register(
      "localization",
      {
        async onRequest({ options }, nuxtApp) {
          options.headers.set("Accept-Language", nuxtApp.$i18n.locale.value);
        },
        priority: 20
      }
    );
  }
});
