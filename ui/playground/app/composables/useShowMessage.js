import { useToast } from "#imports";

export default function() {
  const toast = useToast();

  async function run({ message, severity = "info" }) {
    toast.add({
      severity,
      summary: message,
      life: 3000
    });
  }

  return {
    run
  };
}