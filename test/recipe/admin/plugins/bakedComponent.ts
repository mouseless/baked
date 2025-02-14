export default defineNuxtPlugin({
  name: "bakedComponent",
  setup() {
    const components = import.meta.glob("~/components/*/*.vue");

    return {
      provide: {
        components
      }
    };
  }
});
