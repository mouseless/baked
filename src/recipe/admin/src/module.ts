import { defineNuxtModule, addComponentsDir, addImportsDir, addPlugin, createResolver, installModule } from "@nuxt/kit";
import type { NuxtI18nOptions } from "@nuxtjs/i18n";

export interface ModuleOptions {
  app?: any,
  components?: Components,
  composables: Composables,
  primevue: PrimeVueOptions,
  i18n: NuxtI18nOptions
}

export interface Components {
  DataPanel?: DataPanelOptions,
  MenuPage?: MenuPageOptions,
  Page?: PageOptions,
  ReportPage?: ReportPageOptions
}

export interface DataPanelOptions {
  requiredMessage?: String
}

export interface MenuPageOptions {
  notFoundMessage?: String
}

export interface PageOptions {
  title?: String
}

export interface PrimeVueOptions {
  theme: any,
  locale?: any
}

export interface ReportPageOptions {
  requiredMessage?: String
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
  locale?: String,
  currency?: String,
  suffix?: UseFormatSuffixOptions
}

export interface UseFormatSuffixOptions {
  billions: String,
  millions: String,
  thousands: String
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

    // passing module's options to runtime config for further access
    _nuxt.options.runtimeConfig.public.error = _options.app?.error;
    _nuxt.options.runtimeConfig.public.primevue = _options.primevue;
    _nuxt.options.runtimeConfig.public.components = _options.components;
    _nuxt.options.runtimeConfig.public.composables = _options.composables;

    // by pushing instead of setting, it allows custom css
    _nuxt.options.css.push("primeicons/primeicons.css");
    _nuxt.options.css.push(resolver.resolve("./runtime/assets/theme/datatable.css"));
    _nuxt.options.css.push(resolver.resolve("./runtime/assets/overrides.css"));

    // below settings cannot be overriden
    _nuxt.options.devtools.enabled = false;
    _nuxt.options.experimental.payloadExtraction = false;
    _nuxt.options.features.inlineStyles = false;
    _nuxt.options.ssr = false;

    // default dirs and plugins
    addComponentsDir({ path: resolver.resolve("./runtime/components") });
    addImportsDir(resolver.resolve("./runtime/composables"));
    addPlugin(resolver.resolve("./runtime/plugins/primeVue"));
    addPlugin(resolver.resolve("./runtime/plugins/setupBaked"));
    addPlugin(resolver.resolve("./runtime/plugins/mutex"));
    addPlugin(resolver.resolve("./runtime/plugins/toast"));
    addPlugin(resolver.resolve("./runtime/plugins/trailingSlash"));

    // plugins that comes through the app descriptor
    for(const plugin of _options.app?.plugins ?? []) {
      _nuxt.options.runtimeConfig.public[plugin.name] = plugin;
      addPlugin(resolver.resolve(`./runtime/plugins/${plugin.name}`));
    }

    await installModule("@nuxtjs/i18n", {
      vueI18n: entryProjectResolver.resolve("./i18n.config.ts"),
      restructureDir: false,
      langDir: entryProjectResolver.resolve("./locales"),
      strategy: "no_prefix",
      locales: _options.app?.localization.supportedLanguages.map((l: any) => ({
        code: l.code,
        name: l.name,
        file: entryProjectResolver.resolve(`./locales/${l.code}.json`),
      })),
      defaultLocale: _options.app?.localization.defaultLanguage,
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
    });
  }
});
