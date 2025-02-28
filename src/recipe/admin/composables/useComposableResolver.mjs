export default function() {
  function resolve(name) {
    const nuxtApp = useNuxtApp();
    const composables = nuxtApp.$composables;

    if(!composables[name]) { throw new Error(`Cannot resolve composable '${name}'`); }

    return composables[name];
  }

  return {
    resolve
  };
}
