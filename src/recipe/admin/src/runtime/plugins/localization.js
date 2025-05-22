import { defineNuxtPlugin, useRuntimeConfig } from "#app";
import { resolve } from "pathe";

export default defineNuxtPlugin({
  name: "localization",
  enforce: "pre",
  hooks: {
    "i18n:registerModule": register => {
      const { public: { localization } } = useRuntimeConfig();
      register({
        // TODO - const resolver = createResolver(import.meta.url); e bakılacak
        langDir: resolve(__dirname, "../locales"),
        defaultLocale: localization.defaultLanguage,
        strategy: "no_prefix",
        locales: localization.supportedLanguages.map(l => (
          {
            code: l.code,
            name: l.name,
            file: `${l.code}.json`
          }
        ))
      });
    }
  }
});
