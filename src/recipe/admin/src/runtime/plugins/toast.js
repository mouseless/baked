import { defineNuxtPlugin } from "#app";
import { useToast } from "primevue/usetoast";
import ToastService from "primevue/toastservice";

export default defineNuxtPlugin({
  name: "toast",
  setup(_nuxtApp) {
    _nuxtApp.vueApp.use(ToastService);
    const toast = useToast();

    return {
      provide: {
        toast
      }
    };
  }
});
