import { useLocalization } from "#imports";

export default function({ model }) {
  const { localize: l } = useLocalization();

  const result = {};
  const item = {
    message: "",
    valid: false,
    persist: false,
    severity: "error"
  };

  if(model.role === "Admin" && model.status !== "Active") {
    result.status = {
      ...item,
      persist: true,
      message: l("Spec: Admin role requires Active status")
    };
  }

  return result;
}