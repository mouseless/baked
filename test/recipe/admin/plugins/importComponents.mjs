import { defineAsyncComponent } from "vue";

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

    return {
      provide: {
        components
      }
    };
  }
});
