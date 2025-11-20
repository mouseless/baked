import { useRuntimeConfig } from "#app";
import { createError, useMutex } from "#imports";

export default function() {
  const mutex = useMutex();
  const { public: { auth, composables } } = useRuntimeConfig();

  async function current(
    shouldRefresh = true
  ) {
    const tokenString = localStorage.getItem("token");
    if(!tokenString) { return null; }

    const result = Token(tokenString);

    if(!result.access) { return null; }
    if(!result.refresh) { return null; }

    if(result.refreshIsExpired()) { return null; }
    if(result.accessIsExpired() && shouldRefresh) {
      await refresh();

      return current(false);
    }

    return result;
  }

  async function refresh() {
    await mutex.run(async() => {
      const token = await current(false);
      if(!token?.accessIsExpired()) { return; }

      const result = await $fetch(auth.refreshApiRoute,
        {
          baseURL: composables.useDataFetcher.baseURL,
          method: "POST",
          headers: { "Authorization": `Bearer ${token?.refresh}` }
        }
      );

      setCurrent(result, false);
    });
  }

  function setCurrent(value,
    dispatch = true
  ) {
    if(!value) {
      localStorage.removeItem("token");
    } else {
      localStorage.setItem("token", JSON.stringify(value));
    }

    if(dispatch) {
      globalThis.dispatchEvent(new CustomEvent("token-changed"));
    }
  }

  function onChanged(callback) {
    globalThis.addEventListener("token-changed", callback);
  }

  function offChanged(callback) {
    globalThis.removeEventListener("token-changed", callback);
  }

  function decode(value) {
    const { access } = JSON.parse(value);

    return JSON.parse(atob(access.split(".")[1]));
  }

  return {
    current,
    setCurrent,
    onChanged,
    offChanged,
    decode
  };
};

function Token(tokenString) {
  const { access, refresh, displayName } = JSON.parse(tokenString);

  function accessIsExpired() {
    return isExpired(access);
  }

  function refreshIsExpired() {
    return isExpired(refresh);
  }

  function isExpired(token) {
    try {
      const claims = JSON.parse(atob(token.split(".")[1]));

      return parseInt(claims.exp) * 1000 < Date.now();
    } catch {
      throw createError({ statusCode: 401 });
    }
  }

  return {
    access,
    refresh,
    displayName,
    accessIsExpired,
    refreshIsExpired
  };
}
