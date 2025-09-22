import { inject, provide, ref } from "vue";

export default function() {
  function providePath(name) {
    const path = inject("__bake_path", null);
    provide("__bake_path", path ? `${path}/${name}` : name);
  }

  function injectPath() {
    return inject("__bake_path", "");
  }

  function injectDataDescriptor() {
    return inject("__bake_data_descriptor", null);
  }

  function provideDataDescriptor(value) {
    return provide("__bake_data_descriptor", value);
  }

  function injectData() {
    return {
      ParentData: inject("__bake_injected_data:ParentData", null),
      Custom: inject("__bake_injected_data:Custom", null)
    };
  }

  function provideData(value, key) {
    provide(`__bake_injected_data:${key}`, value);
  }

  function injectLoading() {
    return inject("__bake_loading", ref(false));
  }

  function provideLoading(value) {
    provide("__bake_loading", value);
  }

  function injectPage() {
    return inject("__bake_page");
  }

  function providePage(value) {
    provide("__bake_page", value);
  }

  return {
    injectPath,
    providePath,
    injectDataDescriptor,
    provideDataDescriptor,
    injectData,
    provideData,
    injectLoading,
    provideLoading,
    injectPage,
    providePage
  };
}
