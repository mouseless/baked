import { useRoute } from "#app";

export default function() {
  const route = useRoute();

  return `/login?redirect=${route.fullPath}`;
}