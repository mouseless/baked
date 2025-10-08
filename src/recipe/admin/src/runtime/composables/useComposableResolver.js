import { useNuxtApp } from "#app";

export default function() {
  const { $composables } = useNuxtApp();

  function resolve(name) {
    if(!$composables[name]) { throw new Error(`Cannot resolve composable '${name}'`); }

    return $composables[name]();
  }

  return {
    resolve
  };
}
