import { defineErrorHandler } from "../types/errorHandling";

export default defineErrorHandler({
  order: Number.MAX_VALUE,
  canHandle: function(_, error) {
    return error.statusCode !== 403 && error.statusCode !== 404 && error.statusCode !== 500;
  },
  handle: function(_, error)
  {
    return {
      severity: "error",
      summary: "Beklenmeyen Hata",
      detail: "Lütfen sistem yöneticisi ile iletişime geçiniz",
      life: 3000
    };
  }
});