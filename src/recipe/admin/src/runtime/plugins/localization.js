import { defineNuxtPlugin } from "#app";

export default defineNuxtPlugin({
  name: "localization",
  enforce: "pre",
  async setup(_, nuxt) {
    nuxt.hook("i18n:registerModule", register => {
      register({
        restructureDir: "",
        defaultLocale: "en",
        strategy: "no_prefix",
        locales: [
          {
            code: "en",
            file: "en.json"
          }
        ]
      });
    });
  }
});
