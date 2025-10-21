import * as components from "@@/.baked/components";

export default function() {
  function resolve(type, fallback) {
    return components[type]
      ? components[type]
      : components[fallback];
  }

  return {
    resolve
  };
}
