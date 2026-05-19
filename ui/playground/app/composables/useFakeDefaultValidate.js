export default function useFakeDefaultValidate({ model }) {
  const result = {};
  const validation = {
    required: false,
    valid: false,
    persist: false,
    severity: "error",
    message: ""
  };

  if(model["param-2"] === "error") {
    result["param-2"] = {
      ...validation,
      persist: true,
      message: "Param-2 value is error"
    };
  }

  return result;
}