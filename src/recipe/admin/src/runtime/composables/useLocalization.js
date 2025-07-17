import { useI18n, useRouter, useRuntimeConfig } from "#imports";

export default function(group = "") {
  const { locale, locales: i18nLocales, setLocaleCookie, t } = useI18n();
  const { public: { localization } } = useRuntimeConfig();
  const router = useRouter();

  function getLocales() {
    return localization.supportedLanguages.filter(l => i18nLocales.value.includes(l.code));
  }

  function setLocale(language) {
    setLocaleCookie(language);
    router.go();
  }

  function localize(key, parameters = {}) {
    if(!key) { return; }

    const keyWithGroup = group ? `${group}.${key}` : key;
    const result = t(keyWithGroup, parameters);

    return result === keyWithGroup ? key : result;
  }

  return {
    localize,
    locale,
    getLocales,
    setLocale
  };
}