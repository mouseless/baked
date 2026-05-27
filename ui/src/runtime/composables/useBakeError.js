import { computed, unref } from "vue";
import { useContext } from "#imports";

export default function() {
  const context = useContext();

  function handle() {
    const data = context.injectError({ handle: true });

    return NormalizedError(data);
  }

  function normalize(error) {
    return NormalizedError(error).error;
  }

  return {
    handle,
    normalize
  };
}

function NormalizedError(rawError) {
  const error = computed(() => {
    const e = unref(rawError);
    if(!e) {
      return null;
    }

    if(e.name === "FetchError") {
      const messages = e.data?.errors
        ? Object.values(e.data.errors).flat().join(" ")
        : null;

      return {
        title: e.data?.title ?? e.statusCode ?? "ERROR",
        detail: messages ?? e.data?.detail ?? e.message ?? e.cause ?? "An error occured."
      };
    }

    return {
      title: e.statusCode ?? e.status ?? "ERROR",
      detail: e.message ?? e.cause ?? "An error occured."
    };
  });

  return {
    error,
    rawError
  };
}