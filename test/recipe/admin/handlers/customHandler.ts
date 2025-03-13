import { defineErrorHandler } from "#imports";
import { FetchError } from "ofetch";

export default defineErrorHandler({
  order: 0,
  canHandle: function(_, error) {
    return error instanceof FetchError && error.statusCode !== 401;
  },
  handle: function(_, error)
  {
    const fetchError = error as FetchError;
    if(fetchError.statusCode === 400){
      return {
        severity: "error",
        summary: "Custom Handler",
        detail: "This fetch error is handled by custom handler",
        life: 3000
      };
    }

    return error;
  }
});