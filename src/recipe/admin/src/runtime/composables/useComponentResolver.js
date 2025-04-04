
import * as projectComponents from "@/.baked/components";

export default function() {
  function resolve(type, fallback) {
    type = `Lazy${type}`;

    return projectComponents[type] ? projectComponents[type] : projectComponents[fallback];
  }

  return {
    resolve
  };
}
