import { defineNuxtModule, addComponentsDir, addImportsDir, addPlugin, createResolver, installModule } from "@nuxt/kit";

export interface ModuleOptions {
  app?: any,
  primevue: PrimeVueOptions,
  components?: Components
  composables: Composables
}

export interface Components {
  Bake?: BakeOptions,
  DataPanel?: DataPanelOptions,
  Page?: PageOptions,
  ReportPage?: ReportPageOptions
}

export interface BakeOptions {
  retryFetch?: Boolean
}

export interface DataPanelOptions {
  requiredMessage?: String
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
  baseURL: String
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

    // passing module's options to runtime config for further access
    _nuxt.options.runtimeConfig.public.error = _options.app?.error;
    _nuxt.options.runtimeConfig.public.primevue = _options.primevue;
    _nuxt.options.runtimeConfig.public.components = _options.components;
    _nuxt.options.runtimeConfig.public.composables = _options.composables;

    // by pushing instead of setting, it allows custom css
    _nuxt.options.css.push("primeicons/primeicons.css");

    // below settings cannot be overriden
    _nuxt.options.devtools.enabled = false;
    _nuxt.options.experimental.payloadExtraction = false;
    _nuxt.options.features.inlineStyles = false;
    _nuxt.options.ssr = false;

    // default dirs and plugins
    addComponentsDir({ path: resolver.resolve("./runtime/components"), });
    addImportsDir(resolver.resolve("./runtime/composables"));
    addPlugin(resolver.resolve("./runtime/plugins/addPrimeVue"));
    addPlugin(resolver.resolve("./runtime/plugins/toast"));
    addPlugin(resolver.resolve("./runtime/plugins/mutex"));

    // plugins that comes through the app descriptor
    for(const plugin of _options.app?.plugins ?? []) {
      _nuxt.options.runtimeConfig.public[plugin.name] = plugin;
      addPlugin(resolver.resolve(`./runtime/plugins/${plugin.name}`));
    }

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
