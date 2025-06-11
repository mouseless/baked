import { defineNuxtPlugin, useRuntimeConfig } from "#app";

export default defineNuxtPlugin({
  name: "auth",
  enforce: "pre",
  setup(nuxtApp) {
    const { $fetchInterceptors } = nuxtApp;
    const { public: { composables } } = useRuntimeConfig();

    $fetchInterceptors.register(
      "localization",
      async({ options }, nuxtApp) => {
        options.headers.set("Accept-Language", nuxtApp.$i18n.locale.value);
      },
      20
    );
  }
});