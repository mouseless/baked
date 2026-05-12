export function useValidateDefault({ sections, formData }) {
  const allInputs = sections.flatMap(section =>
    section.inputGroups.flatMap(group => group.inputs)
  );

  return Object.fromEntries(
    allInputs.map(input => {
      const value = formData.value?.[input.name];
      const isEmpty = value === undefined || value === null || String(value).trim() === "";

      return [
        input.name,
        {
          required: !!input.required,
          valid: !input.required || !isEmpty
        }
      ];
    })
  );
}