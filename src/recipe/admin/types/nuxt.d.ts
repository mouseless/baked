import "@nuxt/schema";

declare module "@nuxt/schema" {
  interface NuxtOptions {
    tailwindcss: {
      config: {
        theme: {
          screens: Record<string, string>
        }
      }
    }
  }
}