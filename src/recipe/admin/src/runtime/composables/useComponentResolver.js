import { useNuxtApp } from "#app";

export default function() {
  const { $components } = useNuxtApp();

  function resolve(type, fallback) {
    return $components[type]
      ? $components[type]
      : $components[fallback];
  }

  return {
    resolve
  };
}
