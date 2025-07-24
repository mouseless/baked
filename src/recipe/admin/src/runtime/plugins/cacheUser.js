import { defineNuxtPlugin } from "#app";

export default defineNuxtPlugin({
  name: "cache-user",
  enforce: "pre",
  setup(nuxtApp) {
    const { $fetchInterceptors } = nuxtApp;

    $fetchInterceptors.register(
      "cache-user",
      async(_, next) => {
        return await next();
      },
      // should run before other interceptor
      -10
    );
  }
});
