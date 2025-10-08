import { createError, useNuxtApp } from "#app";

export default function() {
  const { $pages } = useNuxtApp();

  async function fetch(name,
    { throwNotFound } = { throwNotFound: true }
  ) {
    if(!$pages[name]) {
      if(throwNotFound) {
        throw createError({
          statusCode: 404,
          statusMessage: `'${name}' Page Not Found`,
          fatal: true
        });
      } else {
        return null;
      }
    }

    return await $pages[name]();
  }

  return {
    fetch
  };
}
