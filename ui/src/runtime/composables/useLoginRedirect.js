import { useRoute, useRuntimeConfig } from "#app";

export default function() {
  const { public: { auth } } = useRuntimeConfig();

  function computeSync() {
    const route = useRoute();

    return `/${auth.loginPageRoute}?redirect=${route.fullPath}`;
  }

  return {
    computeSync
  };
}
