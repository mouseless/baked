import { useNuxtApp, useRouter, useRuntimeConfig } from "#imports";

export default function({ group } = {}) {
  const { $i18n: { locale, locales: i18nLocales, setLocaleCookie, t } } = useNuxtApp();
  const { public: { localization } } = useRuntimeConfig();
  const router = useRouter();

  function getLocales() {
    return localization.supportedLanguages.filter(l =>
      i18nLocales.value.map(l => l.code).includes(l.code)
    );
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
