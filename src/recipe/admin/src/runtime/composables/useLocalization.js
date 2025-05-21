import { useI18n } from "@nuxtjs/i18n";

export default function() {
  const { locale, locales, setLocale, t } = useI18n();

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
