import { defineNuxtPlugin } from "#app";

export default defineNuxtPlugin({
  name: "localization",
  enforce: "pre",
  setup(nuxtApp) {
    const { $fetchInterceptors } = nuxtApp;

    $fetchInterceptors.register(
      "localization",
      async({ options }, next) => {
        options.headers["Accept-Language"] = nuxtApp.$i18n.locale.value;

        return await next();
      }
    );
  }
});
