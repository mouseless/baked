export default function({ model }) {
  const result = {};

  if(model["param-2"] === "error") {
    result["param-2"] = {
      valid: false,
      persist: true,
      message: "Param-2 value is error"
    };
  }

  return result;
}