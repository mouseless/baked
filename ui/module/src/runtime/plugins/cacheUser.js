import { defineNuxtPlugin, useRuntimeConfig } from "#app";
import { useCache, useToken } from "#imports";

export default defineNuxtPlugin({
  name: "cache-user",
  enforce: "pre",
  setup(nuxtApp) {
    const { public: { cacheUser } } = useRuntimeConfig();
    const { expirationInMinutes } = cacheUser;
    const cache = useCache("cache:user", { expirationInMinutes });
    const { $fetchInterceptors } = nuxtApp;

    $fetchInterceptors.register(
      "cache-user",
      async({ request, options }, next) => {
        if(options.attributes["client-cache"] !== "user") {
          return await next();
        }

        const key = cache.buildKey({ path: request, query: options.query });
        return await cache.getOrCreate({ key, create: next });
      },
      // should run before other interceptors
      -10
    );
  },
  hooks: {
    "app:mounted"() {
      const cache = useCache("cache:user");
      const token = useToken();

      token.onChanged(() => {
        cache.clear();
      });
    }
  }
});
