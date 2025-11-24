import { useToast } from "#imports";

export default function() {
  const toast = useToast();

  async function execute({ message }) {
    toast.add({
      severity: "info",
      summary: message,
      life: 3000
    });

    await Promise.resolve();
  }

  return {
    execute
  };
}