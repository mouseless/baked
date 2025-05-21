import { defineNuxtPlugin, useRuntimeConfig } from "#app";

export default defineNuxtPlugin({
  name: "localization",
  enforce: "pre",
  hooks: {
    "i18n:registerModule": register => {
      const { public: { localization } } = useRuntimeConfig();
      register({
        langDir: resolver.resolve("/locales"),
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
