import { useRoute } from "#app";

export default function() {
  const route = useRoute();

  function compute(property) {
    return property ? route[property] : route;
  }

  return {
    compute
  };
}
