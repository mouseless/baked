import { defineNuxtPlugin } from "#app";
import ConfirmationService from "primevue/confirmationservice";

export default defineNuxtPlugin({
  name: "confirmation",
  enforce: "pre",
  setup(_nuxtApp) {
    _nuxtApp.vueApp.use(ConfirmationService);
  }
});