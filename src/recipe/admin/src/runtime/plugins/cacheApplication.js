import { defineNuxtPlugin } from "#app";
import { useCache } from "#imports";

export default defineNuxtPlugin({
  name: "cache-application",
  enforce: "pre",
  setup(nuxtApp) {
    const cache = useCache("cache:application");
    const { $fetchInterceptors } = nuxtApp;

    $fetchInterceptors.register(
      "cache-application",
      async({ request, options }, next) => {
        if(options.options["client-cache"] !== "application") {
          return await next();
        }

        return await cache.getOrCreate(request, next);
      },
      // should run before other interceptors
      -10
    );
  }
});
