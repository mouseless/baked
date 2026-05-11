import { computed } from "vue";

export function useValidateDefault({ sections, formData }) {
  const allInputs = computed(() =>
    sections.flatMap(section =>
      section.inputGroups.flatMap(group => group.inputs)
    )
  );

  const validateResult = computed(() => {
    return Object.fromEntries(
      allInputs.value.map(input => {
        const value = formData.value?.[input.name];
        const isEmpty =
          value === undefined || value === null || String(value).trim() === "";

        return [
          input.name,
          {
            required: input.required,
            valid: !input.required || !isEmpty
          }
        ];
      })
    );
  });

  const isValid = computed(() =>
    Object.values(validateResult.value).every(v => v.valid)
  );

  return { validateResult, isValid };
}