import PrimeVue, { usePrimeVue } from "primevue/config";
import FocusTrap from "primevue/focustrap";
import Tooltip from "primevue/tooltip";
import { defineNuxtPlugin, useRuntimeConfig } from "#app";
import { ref } from "vue";

export default defineNuxtPlugin(_nuxtApp => {
  const { theme, locale } = useRuntimeConfig().public.primevue;

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

  _nuxtApp.vueApp.use(PrimeVue, options);
  _nuxtApp.vueApp.directive("focustrap", FocusTrap);
  _nuxtApp.vueApp.directive("tooltip", Tooltip);

  // TODO - needs refactoring, load primevue messages
  // only when locale is changed
  _nuxtApp.$router.beforeEach(() => {
    const primevue = usePrimeVue();

    if(_nuxtApp.$i18n) {
      const raw = _nuxtApp.$i18n.tm("primevue");
      const primevueMessages = ref({});

      for(const key in raw) {
        primevueMessages.value[key] = extractText(raw[key]);
      }

      primevue.config.locale = { ...primevue.config.locale, ...primevueMessages.value };
    }
  });
});

function extractText(val) {
  if(typeof val === "string") { return val; }

  if(Array.isArray(val)) {
    return [...val.map(extractText)];
  }

  if(typeof val === "object") {
    return val.loc?.source || val.value || extractText(val.body || "") || "";
  }

  return "";
}