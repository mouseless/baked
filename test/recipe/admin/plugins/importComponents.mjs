import * as Baked from "baked-recipe-admin";

export default defineNuxtPlugin({
  name: "importComponents",
  setup() {
    const projectComponents = import.meta.glob("~/components/*.vue");
    const projectComposables = import.meta.glob("~/composables/*.vue");

    return {
      provide: {
        components: merge({
          bakedFilter: key => !key.startsWith("use"),
          projectImports: projectComponents,
          projectTrimStart: "components/",
          projectTrimEnd: ".vue"
        }),
        composables: merge({
          bakedFilter: key => key.startsWith("use"),
          projectImports: projectComposables,
          projectTrimStart: "composables/",
          projectTrimEnd: ".vue"
        })
      }
    };
  }
});

function merge({ bakedFilter, projectImports, projectTrimStart, projectTrimEnd }) {
  return {
    ...Object.keys(Baked).filter(bakedFilter).reduce((result, name) => {
      result[name] = Baked[name];

      return result;
    }, { }),
    ...Object.keys(projectImports).reduce((result, path) => {
      result[path.slice(path.indexOf(projectTrimStart) + projectTrimStart.length, path.lastIndexOf(projectTrimEnd))] = defineAsyncComponent(projectImports[path]);

      return result;
    }, { })
  };
}
