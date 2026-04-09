import { useRouter } from "#app";

export default function() {
  const router = useRouter();

  async function run({ route, back, expected, actual } = {}) {
    if(expected && actual !== expected) { return; }

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