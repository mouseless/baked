import { useI18n, useRuntimeConfig } from "#imports";

export default function() {
  const { locale, locales: i18nLocales, setLocale, t } = useI18n();
  const { public: { localization } } = useRuntimeConfig();

  const locales = localization.supportedLanguages
    .filter(l => i18nLocales.value.includes(l.code));

  function localize(key, parameters = {}) {
    return t(key, parameters);
  }

  return {
    localize,
    locale,
    locales,
    setLocale
  };
}
