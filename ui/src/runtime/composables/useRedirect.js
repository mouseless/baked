import { useRouter } from "#app";
import { usePathBuilder } from "#imports";

export default function() {
  const router = useRouter();
  const { build } = usePathBuilder();

  async function run({ route, back, expected, actual, ...params } = {}) {
    if(expected && actual !== expected) { return; }

    if(route && Object.keys(params).length > 0) {
      route = build(route, params, { forRoute: true });
    }

    if(back) {
      router.back();
    } else {
      await router.push(route);
    }
  }

  return {
    run
  };
}