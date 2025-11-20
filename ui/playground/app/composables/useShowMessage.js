import { useToast } from "#imports";

export default function() {
  const toast = useToast();

  async function executeAsync({ message }) {
    toast.add({
      severity: "info",
      summary: message,
      life: 3000
    });

    await Promise.resolve();
  }

  return {
    executeAsync
  };
}