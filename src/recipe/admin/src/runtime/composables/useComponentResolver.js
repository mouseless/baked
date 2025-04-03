
import * as bakedComponents from "../components";
import * as clientComponents from "@/components";

export default function() {
  function resolve(type, fallback) {

    return clientComponents[type] ? clientComponents[type] :
      bakedComponents[type] ? bakedComponents[type] : bakedComponents[fallback];
  }

  return {
    resolve
  };
}
