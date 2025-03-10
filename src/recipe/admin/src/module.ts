import { defineNuxtModule, addComponentsDir, addImportsDir, addPlugin, createResolver, installModule } from "@nuxt/kit"

export interface ModuleOptions {
  theme: Object,
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

export default defineNuxtModule<ModuleOptions>({
  meta: {
    name: "baked-recipe-admin",
    configKey: "baked",
  },
  defaults: { },
  async setup(_options, _nuxt) {
    const resolver = createResolver(import.meta.url);

    // this setup runs after `defineNuxtConfig` so it should set only if config
    // is undefined to allow developers override these defaults
    // if not set, arrays and objects are always not-null and empty
    _nuxt.options.css.push("primeicons/primeicons.css");
    _nuxt.options.devtools.enabled ||= false;
    _nuxt.options.experimental.payloadExtraction ||= false;
    _nuxt.options.features.inlineStyles ||= false;
    _nuxt.options.runtimeConfig.public.theme = _options.theme;
    _nuxt.options.runtimeConfig.public.components = _options.components;
    _nuxt.options.ssr ||= false;

    addComponentsDir({
      path: resolver.resolve("./runtime/components"),
    });

    addImportsDir(resolver.resolve("./runtime/composables"));

    addPlugin(resolver.resolve("./runtime/plugins/addPrimevue"));
    addPlugin(resolver.resolve("./runtime/plugins/toast"));

    await installModule("@nuxtjs/tailwindcss", {
      exposeConfig: true,
      cssPath: resolver.resolve("./runtime/assets/tailwind.css"),
      config: {
        content: {
          files: [
            resolver.resolve("./runtime/components/**/*.{vue,mjs,ts}"),
            resolver.resolve("./runtime/*.{mjs,js,ts}"),
          ],
        },
      },
    })
  }
});
