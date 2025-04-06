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

  function loading() {
    return inject("__bake_loading");
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
