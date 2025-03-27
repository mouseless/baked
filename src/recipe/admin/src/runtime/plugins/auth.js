import { defineNuxtPlugin, useNuxtApp, useRoute, useRuntimeConfig } from "#app";
import { ofetch } from "ofetch";
import useToken from "../composables/useToken";
import path from "path";

export default defineNuxtPlugin({
  name: "auth",
  enforce: "pre",
  setup(nuxtApp) {
    const { public: { auth, components } } = useRuntimeConfig();
    const router = nuxtApp.$router;

    globalThis.$fetch = ofetch.create({
      async onRequest({ request, options }) {
        const token = useToken();

        if(options.baseURL !== components.Bake.baseURL) {
          return;
        }

        if(options.headers.has("Authorization") || options.headers.has("authorization")) {
          return;
        }

        if(!request?.includes("refresh") && !request?.includes("login")) {
          const result = await token.current(true);
          options.headers.set("Authorization", "Bearer " + result?.access );
        }
      }
    });

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
