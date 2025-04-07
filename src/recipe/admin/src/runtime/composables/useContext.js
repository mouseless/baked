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
    return inject("__bake_loading", ref(false)).value;
  }

  function setLoading(page) {
    provide("__bake_loading", page);
  }

  function page() {
    return inject("__bake_page");
  }

  function setPage(page) {
    provide("__bake_page", page);
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
