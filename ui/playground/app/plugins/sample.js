import { defineNuxtPlugin } from "#app";

export default defineNuxtPlugin({
  name: "sample",
  enforce: "pre",
  setup() {
    console.log("Sample plugin loaded");
  }
});
