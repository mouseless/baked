import { computed } from "vue";
import { useContext } from "#imports";

export default function() {
  const context = useContext();

  function claim() {
    const data = context.injectError();

    return FormattedError(data);
  }

  return {
    claim
  };
}

function FormattedError(raw) {
  const formatted = computed(() => {
    if(raw.value === null) { return null; }

    const error = raw.value;
    if(error.name === "FetchError") {
      return {
        summary: error.data?.title ?? error.statusCode ?? "ERROR",
        detail: error.data?.detail ?? error.message ?? error.cause ?? "An error occured..."
      };
    }

    return {
      summary: error.statusCode ?? error.status ?? "ERROR",
      detail: error.message ?? error.cause ?? "An error occured..."
    };
  });

  return {
    raw,
    formatted
  };
}