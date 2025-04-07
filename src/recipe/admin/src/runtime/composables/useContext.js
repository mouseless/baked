import { inject, provide, ref } from "vue";

export default function() {
  function add(name) {
    const path = inject("__bake_path", null);
    provide("__bake_path", path ? `${path}/${name}` : name);
  }

  function path() {
    return inject("__bake_path", "");
  }

  function injectedData() {
    return {
      ParentData: inject("__bake_injected_data:ParentData", null),
      Custom: inject("__bake_injected_data:Custom", null)
    };
  }

  function setInjectedData(value, key) {
    provide(`__bake_injected_data:${key}`, value);
  }

  function loading() {
    return inject("__bake_loading", ref(false));
  }

  function setLoading(value) {
    provide("__bake_loading", value);
  }

  function page() {
    return inject("__bake_page");
  }

  function setPage(value) {
    provide("__bake_page", value);
  }

  return {
    add,
    path,
    injectedData,
    setInjectedData,
    loading,
    setLoading,
    page,
    setPage
  };
}
