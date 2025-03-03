import PrimeVue from "primevue/config";
import Tooltip from "primevue/tooltip";
import { defineNuxtPlugin, useRuntimeConfig } from "#app";

export default defineNuxtPlugin(_nuxtApp => {
  const theme = useRuntimeConfig().public.theme;

  _nuxtApp.vueApp.use(PrimeVue, {
    theme: {
      preset: theme,
      options: {
        cssLayer: {
          name: "primevue",
          order: "tailwind-base, primevue, tailwind-utilities"
        }
      }
    }
  });

  _nuxtApp.vueApp.directive("tooltip", Tooltip);
});
