import { createError, useNuxtApp } from "#app";

export default function() {
  const { $pages } = useNuxtApp();

  async function fetch(name) {
    if(!$pages[name]) {
      throw createError({
        statusCode: 404,
        statusMessage: `'${name}' Page Not Found`,
        fatal: true
      });
    }

    return await $pages[name]();
  }

  return {
    fetch
  };
}
