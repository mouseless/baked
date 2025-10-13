import PrimeVue, { usePrimeVue } from "primevue/config";
import FocusTrap from "primevue/focustrap";
import Tooltip from "primevue/tooltip";
import { defineNuxtPlugin, useNuxtApp, useRouter, useRuntimeConfig } from "#app";
import { ref } from "vue";

export default defineNuxtPlugin({
  name: "primeVue",
  setup(nuxtApp) {
    const router = useRouter();
    const { public: { primevue: { theme, locale } } } = useRuntimeConfig();
    const shouldLoadLocale = ref(true);

    const options = {
      theme: {
        preset: theme,
        options: {
          cssLayer: {
            name: "primevue",
            order: "tailwind-base, primevue, tailwind-utilities"
          }
        }
      }
    };

    if(locale) { options.locale = locale; }

    nuxtApp.vueApp.use(PrimeVue, options);
    nuxtApp.vueApp.directive("focustrap", FocusTrap);
    nuxtApp.vueApp.directive("tooltip", Tooltip);

    router.beforeEach(() => {
      if(!shouldLoadLocale.value) {
        return;
      }

      if(!nuxtApp.$i18n) {
        shouldLoadLocale.value = false;
        return;
      }

      loadPrimeVueMessages();
      shouldLoadLocale.value = false;
    });
  }
});

function loadPrimeVueMessages() {
  const { $i18n } = useNuxtApp();
  const primevue = usePrimeVue();

  const raw = $i18n.tm("primevue");
  const primevueMessages = {};

  for(const key in raw) {
    primevueMessages[key] = extractText(raw[key]);
  }

  primevue.config.locale = { ...primevue.config.locale, ...primevueMessages };
}

function extractText(val) {
  if(typeof val === "string") { return val; }

  if(Array.isArray(val)) {
    return [...val.map(extractText)];
  }

  if(typeof val === "object") {
    // 'val.b?.s' is for playwright
    return val.loc?.source || val.b?.s || val.value || extractText(val.body || "") || "";
  }

  return "";
}
