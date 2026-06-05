export default function({ model }) {
  const result = {};

  if(model["param-2"] === "error") {
    result["param-2"] = {
      message: "Param-2 value is error",
      severity: "error",
      valid: false,
      persist: true
    };
  }

  if(model["param-3"] === "info") {
    result["param-3"] = {
      message: "Param-3 value is info",
      severity: "info",
      persist: true
    };
  }

  return result;
}