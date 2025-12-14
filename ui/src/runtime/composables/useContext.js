import { inject, provide, ref } from "vue";

export default function() {
  function providePath(value) {
    provide("__bake_path", value);
  }

  function injectPath() {
    return inject("__bake_path", "");
  }

  function injectContextData() {
    return {
      page: injectPageContext(),
      parent: injectParentContext()
    };
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

  function injectPageContext() {
    return inject("__bake_page_context");
  }

  function providePageContext(value) {
    provide("__bake_page_context", value);
  }

  function injectParentContext() {
    return inject("__bake_parent_context", null);
  }

  function provideParentContext(value) {
    provide("__bake_parent_context", value);
  }

  return {
    injectPath,
    providePath,
    injectContextData,
    injectDataDescriptor,
    provideDataDescriptor,
    injectEvents,
    provideEvents,
    injectExecuting,
    provideExecuting,
    injectLoading,
    provideLoading,
    injectPageContext,
    providePageContext,
    injectParentContext,
    provideParentContext
  };
}
