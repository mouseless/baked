import Aura from "@primeuix/themes/aura";
import { definePreset } from "@primeuix/themes";
import { fileURLToPath } from "node:url";
import { resolve } from "pathe";

const Mouseless = definePreset(Aura, {
  semantic: {
    primary: {
      50: "{teal.100}",
      100: "{teal.200}",
      200: "{teal.300}",
      300: "{teal.400}",
      400: "{teal.500}",
      500: "{teal.600}",
      600: "{teal.700}",
      700: "{teal.800}",
      800: "{teal.900}",
      900: "{teal.950}",
      950: "{black}"
    }
  }
});

// https://nuxt.com/docs/api/configuration/nuxt-config
export default defineNuxtConfig({
  alias: {
    "@utils": resolve(fileURLToPath(new URL(".", import.meta.url)), "../test/utils")
  },
  baked: {
    apiBaseURL: import.meta.env.API_BASE_URL,
    components: {
      Page: {
        title: "Baked Admin"
      }
    },
    composables: {
      useDataFetcher: {
        retry: true
      }
    },
    primevue: {
      theme: Mouseless
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
  logLevel: import.meta.env.BUILD_SILENT === "1" ? "silent" : "info",
  modules: ["@nuxt/eslint", "../src/module"],
  router: { options: { strict: true } }
});
