import { useLocalization } from "#imports";

export default function({ formData }) {
  const { localize: lc } = useLocalization({ group: "ValidatorMessages" });

  const data = formData.value ?? {};
  const result = {};
  const item = {
    severity: "error",
    message: "",
    valid: false
  }

  if(data.role === "Admin" && data.status !== "Active") {
    result.status = { ...item, message: lc("If the role is Admin, the Status must be Active") }
  }

  return result;
}