import { createError, useNuxtApp } from "#app";

export default function() {
  const { $layouts } = useNuxtApp();

  return {
    async fetch(name) {
      if(!$layouts[name]){
        throw createError({
          statusCode: 404,
          statusMessage: `'${name}' Layout Not Found`,
          fatal: true
        });
      }

      return await $layouts[name]();
    }
  };
}
