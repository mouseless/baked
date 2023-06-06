import { defineNuxtConfig } from "nuxt/config";
import { joinURL } from "ufo";

export default defineNuxtConfig({
  typescript: {
    typeCheck: true
  },
  runtimeConfig: {
    public: {
      content: {
        anchorLinks: {
          depth: 0
        }
      },
      baseURL: process.env.BASE_URL,
      githubURL: "/mouseless/do"
    }
  },
  app: {
    baseURL: process.env.BASE_URL,
    head: {
      meta: [
        {
          hid: "og:description",
          property: "og:description",
          content: "An opinionated framework for .NET"
        },
        {
          hid: "og:image",
          property: "og:image",
          content: "https://do.mouseless.codes/favicon.ico"
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
          href: joinURL(process.env.BASE_URL ?? "/", "favicon.ico")
        },
        {
          rel: "stylesheet",
          type: "text/css",
          href: "https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.1.1/css/all.min.css"
        },
        {
          rel: "stylesheet",
          type: "text/css",
          href: "https://mouseless.github.io/brand/assets/css/primary.css"
        }
      ]
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
  css: ["~/assets/styles.scss"],
  modules: ["@nuxt/content"],
  content: {
    markdown: {
      remarkPlugins: {
        "remark-emoji": false
      }
    },
    highlight: {
      // Theme used in all color schemes.
      theme: "dark-plus",
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
  router: {
    options: {
      strict: true
    }
  },
  components: {
    dirs: [{ global: true, path: "~/components/Prose" }, "~/components"]
  },
  dir: {
    public: ".public"
  },
  generate: {
    routes: ["/not-found"]
  }
});
