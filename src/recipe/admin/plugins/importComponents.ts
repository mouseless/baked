export default defineNuxtPlugin({
  name: "importComponents",
  setup() {
    const packageComponents = import.meta.glob("baked-recipe-admin/components/Baked/*.vue");
    const projectComponents = import.meta.glob("~/components/Baked/*.vue");

    return {
      provide: {
        components: {
          ...Object.keys(packageComponents).reduce((result, path) => ({ ...result, [path.slice(path.indexOf("/Baked"))]: packageComponents[path] }), { }),
          ...Object.keys(projectComponents).reduce((result, path) => ({ ...result, [path.slice(path.indexOf("/Baked"))]: projectComponents[path] }), { })
        }
      }
    };
  }
});
