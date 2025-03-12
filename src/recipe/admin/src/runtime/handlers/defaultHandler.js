import { defineErrorHandler } from "../types/errorHandling";

export default defineErrorHandler({
  canHandle: function(_, error) {
    return error.statusCode !== 403 && error.statusCode !== 404 && error.statusCode !== 500;
  },
  handle: function(_, error)
  {
    return {
      severity: "error",
      summary: error.data?.title ?? "Beklenmeyen Hata",
      detail: error.data?.detail ?? error.statusMessage ?? error.message ?? "Lütfen sistem yöneticisi ile iletişime geçiniz",
      life: 3000
    };
  }
});