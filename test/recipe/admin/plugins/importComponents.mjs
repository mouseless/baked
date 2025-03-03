import { defineAsyncComponent } from "vue";
import { defineNuxtPlugin } from "#app";

export default defineNuxtPlugin({
  name: "importComponents",
  setup() {
    const projectComponents = import.meta.glob("~/components/*.vue");
    const bakedComponents = import.meta.glob("~/node_modules/baked-recipe-admin/dist/runtime/components/*.vue");

    const components = {
      ...Object.keys(bakedComponents).reduce((result, path) => {
        result[path.slice(path.indexOf("components/") + "components/".length, path.lastIndexOf(".vue"))] = defineAsyncComponent(bakedComponents[path]);

        return result;
      }, { }),
      ...Object.keys(projectComponents).reduce((result, path) => {
        result[path.slice(path.indexOf("components/") + "components/".length, path.lastIndexOf(".vue"))] = defineAsyncComponent(projectComponents[path]);

        return result;
      }, { })
    };

    const bakedPages = import.meta.glob("~/.baked/*.json");
    const pages = {
      ...Object.keys(bakedPages).reduce((result, path) => {
        result[path.slice(path.indexOf(".baked/") + ".baked/".length, path.lastIndexOf(".json"))] = bakedPages[path];

        return result;
      }, { })
    };

    return {
      provide: {
        components,
        pages
      }
    };
  }
});
