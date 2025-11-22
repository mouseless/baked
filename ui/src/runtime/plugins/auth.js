import { defineNuxtPlugin, useNuxtApp, useRoute, useRuntimeConfig } from "#app";
import { useToken } from "#imports";

export default defineNuxtPlugin({
  name: "auth",
  enforce: "pre",
  setup(nuxtApp) {
    const { public: { auth } } = useRuntimeConfig();
    const token = useToken();
    const { $fetchInterceptors, $router } = nuxtApp;

    $fetchInterceptors.register(
      "auth",
      async({ request, options }, next) => {
        // filters out any api call that already has an authorization header,
        // such as refresh token api call
        if(options.headers["Authorization"] || options.headers["authorization"]) {
          return await next();
        }

        // try get current token
        let result = await token.current(false);
        if(!result || result.accessIsExpired()) {
          // if api is anonymous no need to have a token, will continue
          // anonymously
          if(auth.anonymousApiRoutes.some(route => request?.includes(route))) { return await next(); }

          // force get an access token
          result = await token.current(true);
        }

        options.headers["Authorization"] = `Bearer ${result?.access}`;

        return await next();
      },
      // runs before other interceptors, even early ones like cache
      // this is to prevent any unintended result when user is not authorized while it should be
      //
      // -10 is to leave a room just in case it is needed
      Number.MIN_SAFE_INTEGER + 10
    );

    $router.beforeEach(async to => {
      const token = useToken();

      for(const route of auth.anonymousPageRoutes) {
        const pattern = new RegExp(route);
        if(to.path.match(pattern)?.length > 0) {
          return;
        }
      }

      const toLogin = to.path.includes(auth.loginPage);
      const current = await token.current(!toLogin);

      if(current && toLogin) {
        await $router.replace(to.query.redirect || "/");
      } else if(!current && !toLogin) {
        await $router.replace(`/${auth.loginPageRoute}?redirect=${to.fullPath}`);
      }
    });
  },
  hooks: {
    "app:mounted"() {
      const { $router } = useNuxtApp();
      const { public: { auth } } = useRuntimeConfig();
      const route = useRoute();
      const token = useToken();

      token.onChanged(() => {
        token.current().then(current => {
          if(current) {
            $router.push(route.query.redirect || "/");
          } else {
            $router.push(`/${auth.loginPageRoute}`);
          }
        });
      });
    }
  }
});
