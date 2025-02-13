import type { RouterConfig } from "@nuxt/schema";

export default <RouterConfig>{
  strict: true,
  routes: _routes => [
    {
      path: "/:schema/:id?",
      component: () => import("~/components/Baked/Page.vue")
    }
  ]
};
