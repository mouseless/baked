import Aura from "@primeuix/themes/aura";
import { definePreset } from "@primeuix/themes";
import app from "./.baked/app.json" assert { type: "json" };

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
  baked: {
    app: app,
    components: {
      Bake: {
        retryFetch: true
      },
      Page: {
        title: "Baked Admin"
      }
    },
    composables: {
      useDataFetcher: {
        baseURL: process.env.API_BASE_URL
      }
    },
    primevue: {
      theme: Mouseless,
      locale: { dayNamesMin: [ "sU", "mO", "tU", "wE", "tH", "fR", "sA"] }
    }
  },
  compatibilityDate: "2025-03-01",
  components: {
    dirs: ["~/components"]
  },
  css: [ "~/assets/styles.scss" ],
  // Do NOT remove this line, auto imports are disabled for consistency
  // between local and published package behaviour
  imports: { autoImport: false },
  logLevel: process.env.BUILD_SILENT === "1" ? "silent" : "info",
  modules: [
    "@nuxt/eslint",
    "baked-recipe-admin"
  ],
  router: { options: { strict: true } }
});
