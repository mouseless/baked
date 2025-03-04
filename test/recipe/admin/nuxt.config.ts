import Aura from "@primevue/themes/aura";
import { definePreset } from "@primevue/themes";

const Mouseless = definePreset(Aura, {
  semantic: {
    primary: {
      50: "{red.50}",
      100: "{red.100}",
      200: "{red.200}",
      300: "{red.300}",
      400: "{red.400}",
      500: "{red.500}",
      600: "{red.600}",
      700: "{red.700}",
      800: "{red.800}",
      900: "{red.900}",
      950: "{red.950}"
    }
  }
});

// https://nuxt.com/docs/api/configuration/nuxt-config
export default defineNuxtConfig({
  css: ["assets/styles.scss"],
  baked: {
    theme: Mouseless
  },
  compatibilityDate: "2025-03-01",
  components: {
    dirs: ["~/components"]
  },
  // Do NOT remove this line, auto imports are disabled for consistency
  // between local and published package behaviour
  imports: { autoImport: false },
  logLevel: process.env.SILENT === "1" ? "silent" : "info",
  modules: [
    "@nuxt/eslint",
    "baked-recipe-admin"
  ],
  router: { options: { strict: true } },
  runtimeConfig: {
    public: {
      apiBaseURL: process.env.API_BASE_URL,
      devMode: true,
      title: "Baked Admin"
    }
  }
});
