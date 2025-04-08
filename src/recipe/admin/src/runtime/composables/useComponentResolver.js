
import * as components from "@/.baked/components";

export default function() {
  function resolve(type, fallback) {
    type = `Lazy${type}`;

    return components[type]
      ? components[type]
      : components[fallback];
  }

  return {
    resolve
  };
}
