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

  function injectEvents() {
    return inject("__bake_events");
  }

  function provideEvents(value) {
    provide("__bake_events", value);
  }

  function injectExecuting() {
    return inject("__bake_executing", ref(false));
  }

  function provideExecuting(value) {
    return provide("__bake_executing", value);
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

  function injectParentContext() {
    return inject("__bake_injected_parent_context", null);
  }

  function provideParentContext(value) {
    provide("__bake_injected_parent_context", value);
  }

  return {
    injectPath,
    providePath,
    injectDataDescriptor,
    provideDataDescriptor,
    injectEvents,
    provideEvents,
    injectExecuting,
    provideExecuting,
    injectLoading,
    provideLoading,
    injectPage,
    providePage,
    injectParentContext,
    provideParentContext
  };
}
