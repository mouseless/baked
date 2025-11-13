import { useRoute } from "#app";

/**
 * @deprecated This composable is obsolete and will be removed in a future release.
 * Use `useNuxtRoute()` instead.
 */
export default function() {
  if(import.meta.client) {
    console.warn("[DEPRECATED] `useQuery()` is obsolete. Please use `useNuxtRoute()` instead.");
  }

  const route = useRoute();

  function compute() {
    return route.query;
  }

  return {
    compute
  };
}
