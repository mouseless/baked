import Aura from "@primevue/themes/aura";
import { definePreset } from "@primevue/themes";

const Mouseless = definePreset(Aura, {
  semantic: {
    primary: {
      50: "{red.50}",
      950: "{red.950}"
    }
  }
});

// https://nuxt.com/docs/api/configuration/nuxt-config
export default defineNuxtConfig({
  compatibilityDate: "2024-11-01",
  devtools: { enabled: false },
  components: {
    dirs: [
      "~/components"
    ]
  },
  experimental: {
    payloadExtraction: false
  },
  features: {
    inlineStyles: false
  },
  modules: [
    "@nuxtjs/tailwindcss",
    "@primevue/nuxt-module"
  ],
  primevue: {
    options: {
      theme: {
        preset: Mouseless
      }
    },
    autoImport: true
  },
  router: { options: { strict: true } },
  runtimeConfig: {
    public: {
      apiBaseURL: process.env.API_BASE_URL
    }
  },
  ssr: false
});
