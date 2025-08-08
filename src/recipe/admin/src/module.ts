import { defineNuxtModule, addComponentsDir, addImportsDir, addPlugin, createResolver, installModule } from "@nuxt/kit";
import type { NuxtI18nOptions } from "@nuxtjs/i18n";

export interface ModuleOptions {
  app?: any, // deprecated, will be auto-loaded if not provided
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

export default defineNuxtModule<ModuleOptions>({
  meta: {
    name: "baked-recipe-admin",
    configKey: "baked",
  },
  defaults: { },

  // this setup runs after `defineNuxtConfig` so it should set default values
  // carefully.
  async setup(_options, _nuxt) {
    const resolver = createResolver(import.meta.url);
    const entryProjectResolver = createResolver(_nuxt.options.rootDir);

    let _app = _options.app;
    if (!_app) {
      try {
        _app = require(`${_nuxt.options.rootDir}/.baked/app.json`);
      } catch {
        try {
          _app = require(`${_nuxt.options.rootDir}/app.json`);
        } catch (e) {
          console.warn('[baked-recipe-admin] Could not auto-load app.json:', e);
        }
      }
    }

    // passing module's options to runtime config for further access
    _nuxt.options.runtimeConfig.public.error = _app?.error;
    _nuxt.options.runtimeConfig.public.primevue = _options.primevue;
    _nuxt.options.runtimeConfig.public.components = _options.components;
    _nuxt.options.runtimeConfig.public.composables = _options.composables;

    // by pushing instead of setting, it allows custom css
    _nuxt.options.css.push("primeicons/primeicons.css");
    _nuxt.options.css.push(resolver.resolve("./runtime/assets/theme/datatable.css"));
    _nuxt.options.css.push(resolver.resolve("./runtime/assets/theme/menu.css"));
    _nuxt.options.css.push(resolver.resolve("./runtime/assets/overrides.css"));

    // below settings cannot be overriden
    _nuxt.options.devtools.enabled = false;
    _nuxt.options.experimental.payloadExtraction = false;
    _nuxt.options.features.inlineStyles = false;
    _nuxt.options.ssr = false;

    // default dirs
    addComponentsDir({ path: resolver.resolve("./runtime/components") });
    addImportsDir(resolver.resolve("./runtime/composables"));

    // plugins that comes through the app descriptor
    for(const plugin of _app?.plugins ?? []) {
      _nuxt.options.runtimeConfig.public[plugin.name] = plugin;
      addPlugin(resolver.resolve(`./runtime/plugins/${plugin.name}`));
    }

    //  default plugins (last add, first run)
    addPlugin(resolver.resolve("./runtime/plugins/mutex"));
    addPlugin(resolver.resolve("./runtime/plugins/toast"));
    addPlugin(resolver.resolve("./runtime/plugins/trailingSlash"));
    addPlugin(resolver.resolve("./runtime/plugins/baked"));
    addPlugin(resolver.resolve("./runtime/plugins/primeVue"));
    addPlugin(resolver.resolve("./runtime/plugins/fetch"), {});

    await installModule("@nuxtjs/i18n", {
      vueI18n: entryProjectResolver.resolve("./i18n.config.ts"),
      langDir: entryProjectResolver.resolve("./"),
      strategy: "no_prefix",
      locales: _app?.i18n?.supportedLanguages?.map((i: any) => {
        const files = [
          entryProjectResolver.resolve(`.baked/locale.${i.code}.json`),
          entryProjectResolver.resolve(`./locales/locale.${i.code}.json`)
        ];

        return {
          code: i.code,
          name: i.name,
          files
        }
      }),
      defaultLocale: _app?.i18n?.defaultLanguage?.code,
      detectBrowserLanguage: {
        useCookie: true,
        cookieKey: 'i18n_cookie'
      }
    });
    await installModule("@nuxtjs/tailwindcss", {
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
    })
  }
});
