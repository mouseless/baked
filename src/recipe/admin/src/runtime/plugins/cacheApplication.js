import { defineNuxtPlugin, useRuntimeConfig } from "#app";
import { useCache } from "#imports";

export default defineNuxtPlugin({
  name: "cache-application",
  enforce: "pre",
  setup(nuxtApp) {
    const { public: { cacheApplication } } = useRuntimeConfig();
    const { expirationInMinutes } = cacheApplication;
    const cache = useCache("cache:application", { expirationInMinutes });
    const { $fetchInterceptors } = nuxtApp;

    $fetchInterceptors.register(
      "cache-application",
      async({ request, options }, next) => {
        if(options.attributes["client-cache"] !== "application") {
          return await next();
        }

        const key = cache.buildKey({ path: request, query: options.query });
        return await cache.getOrCreate({ key, create: next });
      },
      // should run before other interceptors
      -10
    );
  }
});
