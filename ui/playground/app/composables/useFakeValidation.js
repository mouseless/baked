export default function({ model }) {
  const result = {};

  if(model["param-2"] === "error") {
    result["param-2"] = {
      valid: false,
      message: "Param-2 value is error",
      persist: true,
      severity: "error"
    };
  }

  if(model["param-3"] === "info") {
    result["param-3"] = {
      valid: true,
      message: "Param-3 value is info",
      persist: true,
      severity: "info"
    };
  }

  return result;
}