import { defineErrorHandler } from "#imports";

export default defineErrorHandler({
  canHandle: function() {
    return true;
  },
  handle: function()
  {
    return {
      severity: "error",
      summary: "Custom Beklenmeyen Hata",
      detail: "Custom Lütfen sistem yöneticisi ile iletişime geçiniz",
      life: 3000
    };
  }
});