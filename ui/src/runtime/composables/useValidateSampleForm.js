import { computed } from "vue";

export function useValidateSampleForm({ formData }) {
  const validateResult = computed(() => {
    const data = formData.value ?? {};
    const result = {};

    if(data.role === "Admin" && data.status !== "Active") {
      result.status = {
        valid: false,
        message: "Admin rolü seçildiğinde durum 'Active' olmak zorundadır."
      };
    }

    return result;
  });

  return { validateResult };
}