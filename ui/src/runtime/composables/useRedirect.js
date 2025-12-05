import { useRouter } from "#app";

export default function() {
  const router = useRouter();

  async function run({ route } = {}) {
    await router.push(route);
  }

  return {
    run
  };
}