import { useRoute } from "#app";

export default function() {
  const route = useRoute();

  function compute() {
    return route;
  }

  return {
    compute
  };
}
