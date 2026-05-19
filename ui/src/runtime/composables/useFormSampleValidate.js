import { useLocalization } from "#imports";

export default function({ model }) {
  const { localize: lc } = useLocalization({ group: "ValidatorMessages" });

  const result = {};
  const item = {
    message: "",
    required: false,
    valid: false,
    persist: false,
    severity: "error"
  };

  if(model.role === "Admin" && model.status !== "Active") {
    result.status = {
      ...item,
      persist: true,
      message: lc("Admin role requires Active status")
    };
  }

  return result;
}