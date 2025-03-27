import { useRuntimeConfig } from "#app";

export default function() {
  const { public: { auth } } = useRuntimeConfig();
  const { public: { components } } = useRuntimeConfig();

  async function login(username, password) {
    return await $fetch(auth.loginRoute,
      {
        baseURL: components?.Bake?.baseURL,
        method: "POST",
        body: { username, password }
      });
  }

  async function refresh(refreshToken) {
    const headers = new Headers();
    headers.set("Authorization", `Bearer ${refreshToken}`);
    return await $fetch(auth.refreshRoute,
      {
        baseURL: components?.Bake?.baseURL,
        method: "POST",
        headers
      });
  }

  return {
    login,
    refresh
  };
};
