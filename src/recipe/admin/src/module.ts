import { defineNuxtModule, addComponentsDir, addImportsDir, addPlugin, createResolver, installModule } from '@nuxt/kit'

export interface ModuleOptions {
  theme: Object
}

export default defineNuxtModule<ModuleOptions>({
  meta: {
    name: 'baked-recipe-admin',
    configKey: 'baked',
  },
  defaults: { },
  async setup(_options, _nuxt) {
    const resolver = createResolver(import.meta.url);

    _nuxt.options.ssr = false
    _nuxt.options.runtimeConfig.public.theme = _options.theme

    addComponentsDir({
      path: resolver.resolve('./runtime/components'),
    });

    addImportsDir(resolver.resolve('./runtime/composables'));

    addPlugin(resolver.resolve('./runtime/plugins/addPrimevue'))

    await installModule('@nuxtjs/tailwindcss', {
      exposeConfig: true,
      cssPath: resolver.resolve('./runtime/assets/tailwind.css'),
      config: {
        darkMode: 'class',
        content: {
          files: [
            resolver.resolve('./runtime/components/**/*.{vue,mjs,ts}'),
            resolver.resolve('./runtime/*.{mjs,js,ts}'),
          ],
        },
      },
    })
  }
});
