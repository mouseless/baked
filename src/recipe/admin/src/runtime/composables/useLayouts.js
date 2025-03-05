import { createError, useNuxtApp } from "#app";

export default function() {
  const { $layouts } = useNuxtApp();

  async function fetch(name) {
    if(!$layouts[name]){
      throw createError({
        statusCode: 404,
        statusMessage: `'${name}' Layout Not Found`,
        fatal: true
      });
    }

    return await $layouts[name]();
  }

  return {
    fetch
  };
}
