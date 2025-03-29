import { useRoute } from "#app";

export default function() {
  function compute() {
    const route = useRoute();

    return `/login?redirect=${route.fullPath}`;
  }

  return {
    compute
  };
}