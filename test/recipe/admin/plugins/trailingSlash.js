import { defineNuxtPlugin } from "#app";

export default defineNuxtPlugin({
  name: "trailingSlash",
  enforce: "pre",
  setup(nuxtApp) {
    const router = nuxtApp.$router;

    router.beforeEach(async to => {
      if(to.path !== "/" && to.path.endsWith("/")) {
        await router.replace({
          path: to.path.slice(0, -1),
          query: to.query
        });
      }
    });
  }
});
