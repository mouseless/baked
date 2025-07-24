import { defineNuxtPlugin } from "#app";

export default defineNuxtPlugin({
  name: "cache-application",
  enforce: "pre",
  setup(nuxtApp) {
    const { $fetchInterceptors } = nuxtApp;

    $fetchInterceptors.register(
      "cache-application",
      async(_, next) => {
        return await next();
      },
      // should run before other interceptor
      -10
    );
  }
});
