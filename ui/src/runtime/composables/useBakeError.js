import { computed, unref } from "vue";
import { useContext } from "#imports";

export default function() {
  const context = useContext();

  function handle() {
    const data = context.injectError({ handle: true });

    return NormalizedError(data);
  }

  function normalize(error) {
    return NormalizedError(error).normalized.value;
  }

  return {
    handle,
    normalize
  };
}

function NormalizedError(raw) {
  const normalized = computed(() => {
    const error = unref(raw);

    if(!error) {
      return null;
    }

    if(error.name === "FetchError") {
      return {
        title: error.data?.title ?? error.statusCode ?? "ERROR",
        detail: error.data?.detail ?? error.message ?? error.cause ?? "An error occured..."
      };
    }

    return {
      title: error.statusCode ?? error.status ?? "ERROR",
      detail: error.message ?? error.cause ?? "An error occured..."
    };
  });

  return {
    raw,
    normalized
  };
}