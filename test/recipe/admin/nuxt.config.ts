import { fileURLToPath } from "url";
import Aura from "@primevue/themes/aura";
import { definePreset } from "@primevue/themes";

// resolve local package absolute path to add it as alias
const bakedRecipeAdmin = fileURLToPath(new URL("../../../src/recipe/admin", import.meta.url));

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
  alias: {
    "baked-recipe-admin": bakedRecipeAdmin
  },
  compatibilityDate: "2024-11-01",
  css: ["~/assets/styles.scss"],
  devtools: { enabled: false },
  components: {
    dirs: [
      "~/components",
      "baked-recipe-admin/components"
    ]
  },
  experimental: {
    payloadExtraction: false
  },
  features: {
    inlineStyles: false
  },
  imports: {
    dirs: [
      // alias didn't work in composables, so full path was given
      `${bakedRecipeAdmin}/composables`
    ]
  },
  logLevel: process.env.SILENT === "1" ? "silent" : "info",
  modules: [
    "@nuxt/eslint",
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
  plugins: [
    // local package plugins
    "baked-recipe-admin/plugins/importComponents"
  ],
  router: { options: { strict: true } },
  runtimeConfig: {
    public: {
      apiBaseURL: process.env.API_BASE_URL
    }
  },
  ssr: false,
  vite: {
    optimizeDeps: {
      include: [
        // primevue components were not rendered correctly when they were
        // imported from the package to include all primevue components
        // upfront this was needed
        "primevue/*"
      ],
      exclude: [
        // adding primevue/* was causing for quill, excluding it workarounds
        // the problem
        "quill"
      ]
    }
  }
});
