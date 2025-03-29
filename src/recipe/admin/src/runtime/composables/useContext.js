import { inject, provide } from "vue";

export default function() {
  function add(name) {
    const path = inject("__bake_path", null);
    provide("__bake_path", path ? `${path}/${name}` : name);
  }

  function path() {
    return inject("__bake_path", "");
  }

  function injectedData() {
    return inject("__bake_injected_data", null);
  }

  function setInjectedData(value) {
    provide("__bake_injected_data", value);
  }

  return {
    add,
    path,
    injectedData,
    setInjectedData
  };
}
