import { addComponentsDir, addImportsDir, addPlugin, createResolver, defineNuxtModule } from "@nuxt/kit";
import type { NuxtI18nOptions } from "@nuxtjs/i18n";
import { pathToFileURL } from "url";

export interface ModuleOptions {
  components?: Components,
  composables: Composables,
  primevue: PrimeVueOptions,
  i18n: NuxtI18nOptions
}

export interface Components {
  Page?: PageOptions,
}

export interface PageOptions {
  title?: String
}

export interface PrimeVueOptions {
  theme: any,
  locale?: any
}

export interface Composables {
  useBreakpoints?: UseBreakpointsOptions,
  useDataFetcher: UseDataFetcherOptions,
  useFormat?: UseFormatOptions
}

export interface UseDataFetcherOptions {
  baseURL: String,
  retry?: RetryOptions | Boolean
}

export interface RetryOptions {
  maxRetry?: Number,
  delay?: Number
}

export interface UseFormatOptions {
  currency?: String
}

export interface UseBreakpointsOptions {
  screens?: ScreenOptions
}

export interface ScreenOptions {
  [key: string]: string;
}

const resolver = createResolver(import.meta.url);

export default defineNuxtModule<ModuleOptions>({
  meta: {
    name: "@mouseless/baked",
    configKey: "baked"
  },
  defaults: { },
  moduleDependencies: {
    "@nuxtjs/i18n": {
      version: "10.1.1",
      defaults: {
        strategy: "no_prefix",
        detectBrowserLanguage: {
          useCookie: true,
          cookieKey: "i18n_cookie"
        }
      }
    },
    "@nuxtjs/tailwindcss": {
      version: "6.14.0",
      defaults: {
        exposeConfig: true,
        cssPath: resolver.resolve("./runtime/assets/tailwind.css"),
        config: {
          content: {
            files: [
              resolver.resolve("./runtime/components/**/*.{vue,mjs,ts}"),
              resolver.resolve("./runtime/*.{mjs,js,ts}")
            ]
          }
        }
      }
    }
  },
  onInstall() {
    // TODO create required project files, e.g., app.vue
  },
  onUpgrade() {
    // Place any update related script here
  },
  // this setup runs after `defineNuxtConfig` so it should set default values
  // carefully.
  async setup(_options, _nuxt) {
    if(process.env.npm_lifecycle_script?.includes("nuxt-module-build")) { return; }

    const entryProjectResolver = createResolver(_nuxt.options.rootDir);

    const appJsonPath = pathToFileURL(entryProjectResolver.resolve("./.baked/app.json"));
    const app = (await import(appJsonPath.href, { with: { type: "json" } })).default;

    _options.composables.useBreakpoints ||= {};
    _options.composables.useBreakpoints.screens ||= {
      "2xs": "340px",
      "xs": "480px",
      "sm": "640px",
      "md": "768px",
      "lg": "1024px",
      "xl": "1280px",
      "2xl": "1536px",
      "3xl": "1920px"
    };

    // passing module's options to runtime config for further access
    _nuxt.options.runtimeConfig.public.error = app?.error;
    _nuxt.options.runtimeConfig.public.primevue = _options.primevue;
    _nuxt.options.runtimeConfig.public.components = _options.components;
    _nuxt.options.runtimeConfig.public.composables = _options.composables;

    // by pushing instead of setting, it allows custom css
    _nuxt.options.css.push("primeicons/primeicons.css");
    _nuxt.options.css.push(resolver.resolve("./runtime/assets/overrides.css"));

    // below settings cannot be overriden
    _nuxt.options.devtools = { enabled: false };
    _nuxt.options.experimental.payloadExtraction = false;
    _nuxt.options.features.inlineStyles = false;
    _nuxt.options.ssr = false;

    // default dirs
    addComponentsDir({ path: resolver.resolve("./runtime/components") });
    addImportsDir(resolver.resolve("./runtime/composables"));

    // plugins that comes through the app descriptor
    for(const plugin of app?.plugins ?? []) {
      _nuxt.options.runtimeConfig.public[plugin.name] = plugin;
      addPlugin(resolver.resolve(`./runtime/plugins/${plugin.name}`));
    }

    // default plugins (last add, first run)
    addPlugin(resolver.resolve("./runtime/plugins/mutex"));
    addPlugin(resolver.resolve("./runtime/plugins/toast"));
    addPlugin(resolver.resolve("./runtime/plugins/trailingSlash"));
    addPlugin(resolver.resolve("./runtime/plugins/baked"));
    addPlugin(resolver.resolve("./runtime/plugins/primeVue"));
    addPlugin(resolver.resolve("./runtime/plugins/fetch"), {});

    // module overrides
    _nuxt.options.vite.optimizeDeps ||= {};
    _nuxt.options.vite.optimizeDeps.noDiscovery = true;

    _nuxt.options.i18n = {
      vueI18n: entryProjectResolver.resolve("./i18n.config.ts"),
      langDir: entryProjectResolver.resolve("./"),
      locales: app?.i18n?.supportedLanguages?.map((i: any) => {
        const files = [
          entryProjectResolver.resolve(`./.baked/locale.${i.code}.json`),
          entryProjectResolver.resolve(`./locales/locale.${i.code}.json`)
        ];

        return {
          code: i.code,
          name: i.name,
          files
        };
      }),
      defaultLocale: app?.i18n?.defaultLanguage?.code
    };

    (_nuxt.options as any).tailwindcss = {
      config: {
        theme: {
          screens: _options.composables.useBreakpoints.screens
        }
      }
    };
  }
});
