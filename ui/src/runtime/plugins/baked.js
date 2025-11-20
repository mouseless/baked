import { defineNuxtPlugin } from "#app";

export default defineNuxtPlugin({
  name: "baked",
  enforce: "pre",
  setup() {
    const bakedComposables = import.meta.glob("../composables/*");
    const projectComposables = import.meta.glob("@/composables/*");

    const pages = import.meta.glob("@@/.baked/**/*.page.json");
    const layouts = import.meta.glob("@@/.baked/**/*.layout.json");

    return {
      provide: {
        composables: merge({
          bakedImports: bakedComposables,
          projectImports: projectComposables,
          trimStart: "composables/",
          trimEnd: "."
        }),
        pages: jsonFiles(pages, ".baked/", ".page.json"),
        layouts: jsonFiles(layouts, ".baked/", ".layout.json")
      }
    };
  },
  hooks: {
    "app:created"(app) {
      const pages = app.$nuxt.$pages;

      Object.keys(pages).forEach(key => {
        app.$nuxt.$router.addRoute({
          name: key,
          path: keyToRoutePattern(key),
          component: () => import("../components/Page.vue")
        });
      });
    }
  }
});

function merge({ bakedImports, projectImports, trimStart, trimEnd }) {
  return {
    ...Object.keys(bakedImports).reduce((result, path) => {
      result[path.slice(path.indexOf(trimStart) + trimStart.length, path.lastIndexOf(trimEnd))] = bakedImports[path];

      return result;
    }, { }),
    ...Object.keys(projectImports).reduce((result, path) => {
      result[path.slice(path.indexOf(trimStart) + trimStart.length, path.lastIndexOf(trimEnd))] = projectImports[path];

      return result;
    }, { })
  };
}

function jsonFiles(imports, trimStart, trimEnd) {
  return {
    ...Object.keys(imports).reduce((result, path) => {
      result[path.slice(path.indexOf(trimStart) + trimStart.length, path.lastIndexOf(trimEnd))] = imports[path];

      return result;
    }, { })
  };
}

function keyToRoutePattern(key) {
  if(key === "index") {
    return "/";
  }

  // AI GEN
  // convert route part 'route/[id]' to 'route/:id'
  return `/${key.replace(/\[([^\]]+)\]/g, ":$1([a-zA-Z0-9-]+)")}`;
}