import { useLocalization } from "#imports";

const capitalize = str => str.charAt(0).toUpperCase() + str.slice(1);

export default function useFakeValidateDefault({ inputData, formData }) {
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

  inputData.map(input => {
    const value = formData?.[input.name];
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

    if(input.name === "param-2" && formData["param-2"] === "error") {
      result["param-2"] = {
        ...validation,
        persist: true,
        message: `${label} value is error`
      };
    }

    return input;
  });

  return result;
}