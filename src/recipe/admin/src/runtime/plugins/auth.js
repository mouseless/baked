import { defineNuxtPlugin, useNuxtApp, useRoute, useRuntimeConfig } from "#app";
import useToken from "../composables/useToken";

export default defineNuxtPlugin({
  name: "auth",
  enforce: "pre",
  setup(nuxtApp) {
    const { $fetchInterceptors } = nuxtApp;
    const { public: { auth } } = useRuntimeConfig();
    const token = useToken();
    const router = nuxtApp.$router;

    $fetchInterceptors.register(
      "auth",
      async({ request, options }) => {
        // filters out any api call that already has an authorization header,
        // such as refresh token api call
        if(options.headers.has("Authorization") || options.headers.has("authorization")) { return; }

        // try get current token
        let result = await token.current(false);
        if(!result || result.accessIsExpired()) {
          // if api is anonymous no need to have a token, will continue
          // anonymously
          if(auth.anonymousApiRoutes.some(route => request?.includes(route))) { return; }

          // force get an access token
          result = await token.current(true);
        }

        options.headers.set("Authorization", "Bearer " + result?.access );
      },
      10
    );

    router.beforeEach(async(to, _) => {
      for(const route of auth.anonymousPageRoutes) {
        const pattern = new RegExp(route);
        if(to.path.match(pattern)?.length > 0) {
          return;
        }
      }

      const token = useToken();
      const toLogin = to.path.includes(auth.loginPage);
      const current = await token.current(!toLogin);

      if(current && toLogin) {
        await router.replace(to.query.redirect || "/");
      } else if(!current && !toLogin) {
        await router.replace(`/${auth.loginPageRoute}?redirect=${to.fullPath}`);
      }
    });
  },
  hooks: {
    "app:mounted"() {
      const { public: { auth } } = useRuntimeConfig();
      const nuxtApp = useNuxtApp();
      const router = nuxtApp.$router;
      const route = useRoute();
      const token = useToken();

      token.onChanged(() => {
        token.current().then(current => {
          if(current) {
            router.push(route.query.redirect || "/");
          } else {
            router.push(`/${auth.loginPageRoute}`);
          }
        });
      });
    }
  }
});
