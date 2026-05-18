import { useLocalization } from "#imports";

const capitalize = str => str.charAt(0).toUpperCase() + str.slice(1);

export default function useValidateDefault({ inputData, formData }) {
  const { localize: lc } = useLocalization({ group: "ValidatorMessages" });
  const { localize: l } = useLocalization({ });

  const result = {};
  const validation = {
    required: false,
    valid: false,
    persist: false,
    severity: "error",
    message: ""
  };

  inputData.value.map(input => {
    const value = formData.value?.[input.name];
    const isEmpty = value === undefined || value === null || String(value).trim() === "";
    const label = input.component?.schema?.label || capitalize(input.name);

    result[input.name] = {
      ...validation,
      required: !!input.required,
      valid: !input.required || !isEmpty,
      persist: !(input.required && isEmpty)
    };

    if(input.required && isEmpty) {
      result[input.name].message = lc("{label} cannot be empty", { label: l(label) });
    }

    return input;
  });

  return result;
}