import { defineNuxtPlugin, useRuntimeConfig } from "#app";
import { useCache } from "#imports";

export default defineNuxtPlugin({
  name: "cache-application",
  enforce: "pre",
  setup(nuxtApp) {
    const { public: { cacheApplication } } = useRuntimeConfig();
    const cache = useCache("cache:application", cacheApplication.expirationInMinutes);
    const { $fetchInterceptors } = nuxtApp;

    $fetchInterceptors.register(
      "cache-application",
      async({ request, options }, next) => {
        if(options.options["client-cache"] !== "application") {
          return await next();
        }

        return await cache.getOrCreate(cache.buildKey(request, options.query), next);
      },
      // should run before other interceptors
      -10
    );
  }
});
