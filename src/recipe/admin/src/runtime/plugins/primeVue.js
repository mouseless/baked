import PrimeVue from "primevue/config";
import FocusTrap from "primevue/focustrap";
import Tooltip from "primevue/tooltip";
import { defineNuxtPlugin, useRuntimeConfig } from "#app";

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
});
