import { useI18n, useRuntimeConfig } from "#imports";
import { usePrimeVue } from "primevue";
import { ref } from "vue";

export default function() {
  const { locale, locales: i18nLocales, setLocale: i18nSetLocales, t, tm } = useI18n();
  const primevue = usePrimeVue();
  const { public: { localization } } = useRuntimeConfig();

  const locales = localization.supportedLanguages
    .filter(l => i18nLocales.value.includes(l.code));

  async function setLocale(language) {
    await i18nSetLocales(language);
    const raw = tm("primevue");
    const primevueMessages = ref({});

    for(const key in raw) {
      primevueMessages.value[key] = extractText(raw[key]);
    }

    primevue.config.locale = { ...primevue.config.locale, ...primevueMessages.value };
  }

  function localize(key, parameters = {}) {
    // When there are special characters such as '{' in the key, it throws an error.
    try { return t(key, parameters); }
    catch { return key; }
  }

  return {
    localize,
    locale,
    locales,
    setLocale
  };
}

function extractText(val) {
  if(typeof val === "string") return val;
  if(Array.isArray(val)) {
    return [...val.map(extractText)];
  }
  if(typeof val === "object") {
    return val.loc?.source || val.value || extractText(val.body || "") || "";
  }

  return "";
}