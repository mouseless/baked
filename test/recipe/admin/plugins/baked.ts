export default defineNuxtPlugin({
  name: "baked",
  setup() {
    const components = import.meta.glob("~/components/*/*.vue");

    return {
      provide: {
        components
      }
    };
  }
});
