import { defineNuxtModule, addComponentsDir, addImportsDir, addPlugin, createResolver, installModule } from "@nuxt/kit";

export interface ModuleOptions {
  app?: any,
  primevue: PrimeVueOptions,
  components?: Components
}

export interface Components {
  Bake?: BakeOptions,
  Page?: PageOptions
}

export interface BakeOptions {
  baseURL?: String,
  retryFetch?: Boolean
}

export interface PageOptions {
  title?: String
}

export interface PrimeVueOptions {
  theme: any,
  locale?: any
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

    // by pushing instead of setting, it allows custom css
    _nuxt.options.css.push("primeicons/primeicons.css");

    // below settings cannot be overriden
    _nuxt.options.devtools.enabled = false;
    _nuxt.options.experimental.payloadExtraction = false;
    _nuxt.options.features.inlineStyles = false;
    _nuxt.options.ssr = false;

    addComponentsDir({ path: resolver.resolve("./runtime/components"), });
    addImportsDir(resolver.resolve("./runtime/composables"));
    addImportsDir(resolver.resolve("./runtime/types"));
    addPlugin(resolver.resolve("./runtime/plugins/addPrimeVue"));
    addPlugin(resolver.resolve("./runtime/plugins/toast"));

    for(const plugin of _options.app?.plugins ?? []) {
      _nuxt.options.runtimeConfig.public[plugin.name] = plugin;
      
      const pluginPath = `./runtime/plugins/${plugin.name}`;
      addPlugin(resolver.resolve(pluginPath));
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
