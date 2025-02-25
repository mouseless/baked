import Baked from "baked-recipe-admin";

export default defineNuxtPlugin({
  name: "importComponents",
  setup() {
    const projectComponents = import.meta.glob("~/components/*.vue");

    const components = {
      ...Baked,
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
