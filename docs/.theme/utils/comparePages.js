import { clean, compare } from "semver";

export default function(a, b, options = {}) {
  const { by = "title", order = "asc", version = false } = options;

  const direction = order === "asc" ? 1 : -1;
  if(version) {
    return compare(clean(a[by] + ".0"), clean(b[by] + ".0")) * direction;
  } else {
    if(a[by] < b[by]) { return -1 * direction; }
    if(a[by] > b[by]) { return 1 * direction; }
  }
  return 0;
}