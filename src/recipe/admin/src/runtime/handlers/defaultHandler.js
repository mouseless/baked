import { defineErrorHandler } from "../types/errorHandling";

export default defineErrorHandler({
  order: Number.MAX_VALUE,
  canHandle: function(_, error) {
    return error.statusCode !== 403 && error.statusCode !== 404 && error.statusCode !== 500;
  },
  handle: function()
  {
    return {
      severity: "error",
      summary: "Unexpected Error",
      detail: "Please contact system administrator",
      life: 3000
    };
  }
});