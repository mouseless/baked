export default function() {
  function resolve(type, fallback) {
    const nuxtApp = useNuxtApp();
    const components = nuxtApp.$components;

    return components[`/Baked/${type}.vue`]
      ? defineAsyncComponent(components[`/Baked/${type}.vue`])
      : defineAsyncComponent(components[`/Baked/${fallback}.vue`]);
  }

  return {
    resolve
  };
}
