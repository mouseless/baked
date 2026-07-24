import { useError } from "#app";

export default function() {
  const error = useError();

  /**
   * Explicit return type required: without it, nuxt-module-build's
   * declaration output tries to reference Nuxt's internal type path
   * (~/node_modules/nuxt/dist/app/types), which isn't resolvable once
   * this module is published and installed elsewhere (TS2883).
   *
   * @returns {import('vue').Ref<import('#app').NuxtError | undefined>}
   */
  function compute() {
    return error;
  }

  return {
    compute
  };
}
