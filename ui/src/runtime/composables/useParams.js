import { useRoute } from "#app";

export default function() {
  const route = useRoute();

  function computeSync() {
    return route.params;
  }

  return {
    computeSync
  };
}
