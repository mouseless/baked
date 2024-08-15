import { defineNuxtConfig } from "nuxt/config";
import { joinURL } from "ufo";

export default defineNuxtConfig({
  app: {
    baseURL: process.env.NUXT_PUBLIC_BASE_URL,
    head: {
      meta: [
        { charset: "utf-8" },
        {
          name: "viewport",
          content: "width=device-width, initial-scale=1"
        },
        {
          hid: "og:type",
          property: "og:type",
          content: "website"
        },
        {
          hid: "og:locale",
          property: "og:locale",
          content: "en_US"
        },
        {
          hid: "og:site_name",
          property: "og:site_name",
          content: "Baked (Objects)"
        },
        {
          hid: "og:description",
          property: "og:description",
          content: "An opinionated framework for .NET"
        },
        {
          hid: "og:image",
          property: "og:image",
          content: "https://baked.mouseless.codes/favicon.ico"
        },
        {
          hid: "og:image:width",
          property: "og:image:width",
          content: "50"
        },
        {
          hid: "og:image:height",
          property: "og:image:height",
          content: "50"
        }
      ],
      link: [
        {
          rel: "icon",
          type: "image/x-icon",
          href: joinURL(process.env.NUXT_PUBLIC_BASE_URL ?? "/", "favicon.ico")
        },
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
    highlight: {
      // Theme used in all color schemes.
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
        "bash"
      ]
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
  generate: {
    routes: ["/not-found"]
  },
  modules: [
    "@nuxt/content",
    "@pinia/nuxt"
  ],
  runtimeConfig: {
    public: {
      mdc: {
        headings: {
          anchorLinks: {
            h1: false,
            h2: false,
            h3: false,
            h4: false,
            h5: false,
            h6: false
          }
        }
      },
      baseURL: "",
      githubURL: "/mouseless/baked"
    }
  },
  vite: {
    css: {
      preprocessorOptions: {
        scss: {
          additionalData: "@import \"@/assets/variables.scss\"; @import \"@/assets/mixins.scss\";"
        }
      }
    }
  },
  compatibilityDate: "2024-08-15"
});
