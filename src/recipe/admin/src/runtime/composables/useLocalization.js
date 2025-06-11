import { useI18n, useRuntimeConfig } from "#imports";
import { usePrimeVue } from "primevue";
import { ref, computed } from "vue";

export default function(group = "") {
  const { locale, locales: i18nLocales, setLocale: i18nSetLocales, t, tm } = useI18n();
  const primevue = usePrimeVue();
  const { public: { localization } } = useRuntimeConfig();

  const locales = computed(() => {
    return localization.supportedLanguages.filter(l => i18nLocales.value.includes(l.code));
  });

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
    if(!key) { return; }

    const keyWithGroup = group ? `${group}.${key}` : key;

    return t(keyWithGroup, parameters);
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