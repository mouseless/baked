export default function() {
  function resolve(type, fallback) {
    const nuxtApp = useNuxtApp();
    const components = nuxtApp.$components;

    return components[type]
      ? components[type]
      : components[fallback];
  }

  return {
    resolve
  };
}
