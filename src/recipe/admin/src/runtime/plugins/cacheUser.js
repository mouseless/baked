import { defineNuxtPlugin, useRuntimeConfig } from "#app";
import { useCache, useToken } from "#imports";

export default defineNuxtPlugin({
  name: "cache-user",
  enforce: "pre",
  setup(nuxtApp) {
    const { public: { cacheUser } } = useRuntimeConfig();
    const cache = useCache("cache:user", cacheUser.expirationInMinutes);
    const { $fetchInterceptors } = nuxtApp;

    $fetchInterceptors.register(
      "cache-user",
      async({ request, options }, next) => {
        if(options.options["client-cache"] !== "user") {
          return await next();
        }

        return await cache.getOrCreate(`${request}?${new URLSearchParams(options.query)}`, next);
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
