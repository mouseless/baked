import { defineNuxtPlugin, useNuxtApp, useRoute, useRuntimeConfig } from "#app";
import useToken from "../composables/useToken";

export default defineNuxtPlugin({
  name: "auth",
  enforce: "pre",
  setup(nuxtApp) {
    const { public: { auth } } = useRuntimeConfig();
    const router = nuxtApp.$router;

    router.beforeEach(async(to, _) => {
      for(const route of auth.anonymousRoutes) {
        const pattern = new RegExp(route);
        if(to.path.match(pattern)?.length > 0) {
          return;
        }
      }

      const token = useToken();
      const toLogin = to.path.includes("login");
      const current = await token.current(!toLogin);

      if(current && toLogin) {
        await router.replace(to.query.redirect || "/");
      } else if(!current && !toLogin) {
        await router.replace(`/login?redirect=${to.fullPath}`);
      }
    });
  },
  hooks: {
    "app:mounted"() {
      const nuxtApp = useNuxtApp();
      const router = nuxtApp.$router;
      const route = useRoute();
      const token = useToken();

      token.onChanged(() => {
        token.current().then(current => {
          if(current) {
            router.push(route.query.redirect || "/");
          } else {
            router.push("/login");
          }
        });
      });
    }
  }
});
