import { inject, provide, ref } from "vue";

export default function() {
  function add(name) {
    const path = inject("__bake_path", null);
    provide("__bake_path", path ? `${path}/${name}` : name);
  }

  function path() {
    return inject("__bake_path", "");
  }

  function injectDataDescriptor() {
    return inject("__bake_data_descriptor", null);
  }

  function provideDataDescriptor(value) {
    return provide("__bake_data_descriptor", value);
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

  function articleOverflow() {
    return inject("__bake_article_overflow", ref(false));
  }

  function setArticleOverflow(value) {
    provide("__bake_article_overflow", value);
  }

  return {
    add,
    path,
    injectDataDescriptor,
    provideDataDescriptor,
    injectedData,
    setInjectedData,
    loading,
    setLoading,
    page,
    setPage,
    articleOverflow,
    setArticleOverflow
  };
}
