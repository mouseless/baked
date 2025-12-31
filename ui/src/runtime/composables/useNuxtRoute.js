import { useRoute } from "#app";

export default function() {
  const route = useRoute();

  function computeSync({ property } = {}) {
    return property ? route[property] : route;
  }

  return {
    computeSync
  };
}
