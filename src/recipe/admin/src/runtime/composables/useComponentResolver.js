
import * as bakedComponents from "../components";
import * as projectComponents from "@/components";

export default function() {
  function resolve(type, fallback) {
    type = `Lazy${type}`;

    return projectComponents[type] ? projectComponents[type] :
      bakedComponents[type] ? bakedComponents[type] : bakedComponents[fallback];
  }

  return {
    resolve
  };
}
