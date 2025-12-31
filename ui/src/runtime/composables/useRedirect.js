import { useRouter } from "#app";

export default function() {
  const router = useRouter();

  async function run({ route, back } = {}) {
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
