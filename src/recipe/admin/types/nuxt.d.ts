import "@nuxt/schema";

// This was added to hide the warnings that appear when we try to override the
// options due to the tailwindcss module
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