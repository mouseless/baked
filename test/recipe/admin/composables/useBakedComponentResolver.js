export default function() {
  function resolve(type, fallback) {
    const nuxtApp = useNuxtApp();
    const components = nuxtApp.$components;

    return components[`/components/Baked/${type}.vue`]
      ? defineAsyncComponent(components[`/components/Baked/${type}.vue`])
      : defineAsyncComponent(components[`/components/Baked/${fallback}.vue`]);
  }

  return {
    resolve
  };
}
