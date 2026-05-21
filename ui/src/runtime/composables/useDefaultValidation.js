import { useLocalization } from "#imports";

const capitalize = str => str.charAt(0).toUpperCase() + str.slice(1);

export default function useDefaultValidation({ inputs, model }) {
  const { localize: lc } = useLocalization({ group: "useDefaultValidation" });
  const { localize: l } = useLocalization();

  const result = {};
  const validation = {
    valid: false,
    persist: false,
    severity: "error",
    message: ""
  };

  inputs.map(input => {
    const value = model?.[input.name];
    const isEmpty = value === undefined || value === null || String(value).trim() === "";
    const label = input.component?.schema?.label?.text || capitalize(input.name);

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