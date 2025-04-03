import { defineNuxtPlugin } from "#app";

export default defineNuxtPlugin({
  name: "setupBaked",
  setup() {
    const bakedComposables = import.meta.glob("../composables/*");
    const projectComposables = import.meta.glob("@/composables/*");

    const pages = import.meta.glob("@/.baked/*.page.json");
    const layouts = import.meta.glob("@/.baked/*.layout.json");

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
