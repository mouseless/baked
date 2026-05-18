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

    if(input.name == "surname" && formData.surname == "huhu") {
      result.surname.message = "huhuhuuhh";
      result.surname.valid = false;
    }

    return input;
  });

  return result;
}