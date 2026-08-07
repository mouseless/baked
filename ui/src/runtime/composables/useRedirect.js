import { useRouter } from "#app";
import { usePathBuilder } from "#imports";

export default function() {
  const router = useRouter();
  const { buildDetailed } = usePathBuilder();

  async function run({ route, back, expected, actual, query, includeQuery, excludeQuery, ...params } = {}) {
    if(expected && actual !== expected) { return; }

    if(route && Object.keys(params).length > 0) {
      const { path, usedKeys } = buildDetailed(route, params, { forRoute: true });
      route = path;

      if(query) {
        const queryParams = Object.fromEntries(
          Object.entries(params).filter(([key, value]) =>
            shouldInclude(key, {
              include: includeQuery,
              exclude: [...(excludeQuery ?? []), ...usedKeys]
            }) && value !== undefined && value !== null
          )
        );

        if(Object.keys(queryParams).length > 0) {
          route += `?${new URLSearchParams(queryParams)}`;
        }
      }
    }

    if(back) {
      router.back();
    } else {
      await router.push(route);
    }
  }

  function shouldInclude(key, { include, exclude } = {}) {
    if(include?.length) return include.includes(key);

    return !exclude?.includes(key);
  }

  return {
    run
  };
}