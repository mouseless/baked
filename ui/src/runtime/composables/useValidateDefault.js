export default function useValidateDefault({ sections, formData }) {
  const allInputs = sections.flatMap(section =>
    section.inputGroups.flatMap(group => group.inputs)
  );
  const result = {};
  const validation = {
    message: "",
    required: false,
    valid: false,
    persist: false,
    severity: "error"
  };

  allInputs.map(input => {
    const value = formData.value?.[input.name];
    const isEmpty = value === undefined || value === null || String(value).trim() === "";

    result[input.name] = {
      ...validation,
      required: !!input.required,
      valid: !input.required || !isEmpty
    };

    return input;
  });

  return result;
}