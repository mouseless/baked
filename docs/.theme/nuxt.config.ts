import { defineNuxtConfig } from "nuxt/config";

export default defineNuxtConfig({
  app: {
    head: {
      meta: [
        { charset: "utf-8" },
        {
          name: "viewport",
          content: "width=device-width, initial-scale=1"
        },
        {
          id: "og:type",
          property: "og:type",
          content: "website"
        },
        {
          id: "og:locale",
          property: "og:locale",
          content: "en_US"
        },
        {
          id: "og:site_name",
          property: "og:site_name",
          content: "Baked"
        },
        {
          id: "og:description",
          property: "og:description",
          content: "An opinionated framework for .NET"
        },
        {
          id: "og:image",
          property: "og:image",
          content: "https://baked.mouseless.codes/favicon.ico"
        },
        {
          id: "og:image:width",
          property: "og:image:width",
          content: "50"
        },
        {
          id: "og:image:height",
          property: "og:image:height",
          content: "50"
        }
      ],
      link: [
        {
          rel: "stylesheet",
          type: "text/css",
          href: "https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.1.1/css/all.min.css"
        },
        {
          rel: "stylesheet",
          type: "text/css",
          href: "https://brand.mouseless.codes/assets/css/default.css"
        }
      ]
    }
  },
  components: {
    global: true,
    dirs: ["~/components/Prose", "~/components"]
  },
  content: {
    build: {
      markdown: {
        highlight: {
          theme: "slack-dark",
          preload: [
            "diff",
            "ts",
            "js",
            "css",
            "java",
            "markdown",
            "sql",
            "xml",
            "json",
            "csharp",
            "md",
            "bash",
            "dockerfile"
          ]
        }
      }
    },
    experimental: {
      nativeSqlite: true
    },
    renderer: {
      anchorLinks: {
        h1: false,
        h2: false,
        h3: false,
        h4: false,
        h5: false,
        h6: true
      }
    }
  },
  css: ["~/assets/styles.scss"],
  devtools: { enabled: false },
  dir: {
    public: ".public"
  },
  experimental: { payloadExtraction: false },
  features: {
    inlineStyles: false
  },
  modules: [
    "@nuxt/content",
    "@nuxt/eslint",
    "@pinia/nuxt"
  ],
  nitro: {
    prerender: {
      routes: ["/not-found"]
    }
  },
  runtimeConfig: {
    public: {
      baseURL: "",
      githubURL: "/mouseless/baked",
      matrixURL: "#baked:mouseless.org"
    }
  },
  vite: {
    css: {
      preprocessorOptions: {
        scss: {
          additionalData: `
            @use "@/assets/variables.scss" as *;
            @use "@/assets/mixins.scss" as *;
          `
        }
      }
    }
  },
  compatibilityDate: "2024-08-15"
});
