import { useLocalization } from "#imports";

const capitalize = str => str.charAt(0).toUpperCase() + str.slice(1);

export default function useValidateDefault({ sections, formData }) {
  const { localize: l } = useLocalization({});

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
    const inputLabel = input.component?.schema?.label || capitalize(input.name) || "some";

    let message = "";
    if(input.required && isEmpty) {
      message = `${l(inputLabel)} boş olamaz`;
    }

    result[input.name] = {
      ...validation,
      required: !!input.required,
      valid: !input.required || !isEmpty,
      message: message,
      persist: !(input.required && isEmpty)
    };

    return input;
  });

  return result;
}