import { defineNuxtPlugin } from "#app";

export default defineNuxtPlugin({
  name: "localization",
  enforce: "pre",
  async setup(_, nuxt) {
    nuxt.hook("i18n:registerModule", register => {
      register({
        langDir: resolver.resolve("/locales"),
        defaultLocale: "en",
        strategy: "no_prefix",
        locales: [
          {
            code: "en",
            name: "English",
            file: "en.json"
          }
        ]
      });
    });
  }
});
