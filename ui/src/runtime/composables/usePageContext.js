import { onBeforeUnmount, ref } from "vue";
import { useContext } from "#imports";

export default function({ trackLoading = true } = {}) {
  const context = useContext();

  const page = context.injectPageContext();
  const pending = context.injectPagePending();
  const loading = trackLoading ? context.injectLoading() : ref(false);
  const marked = new Set();

  if(trackLoading) {
    onBeforeUnmount(() => {
      for(const key of marked) {
        pending.delete(key);
      }
    });
  }

  function get(key) {
    return page[key];
  }

  function set(key, value) {
    if(loading.value) {
      pending.add(key);
      marked.add(key);

      return;
    }

    page[key] = value;
    pending.delete(key);
    marked.delete(key);
  }

  function isPending(key) {
    return pending.has(key);
  }

  return {
    get,
    set,
    isPending
  };
}