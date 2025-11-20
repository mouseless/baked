import { useRuntimeConfig } from "#app";

export default function() {
  async function send({ path, method, params, headers, query, body }) {
    const { public: { composables } } = useRuntimeConfig();

    const result = await $fetch(buildPath(path, params),
      {
        baseURL: composables.useDataFetcher.baseURL,
        method: method,
        headers: headers,
        query: query,
        body: body
      });

    return result;
  }

  function buildPath(path, params) {
    Object.entries(params).forEach(([key, value]) => {
      // AI-GEN
      // match either {key} or {anything:key}
      const regex = new RegExp(`\\{(?:[\\w-]+:)?${key}\\}`, "g");
      path = path.replace(regex, value);
    });
    return path;
  }

  return {
    send
  };
}