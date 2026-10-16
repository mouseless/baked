import { ref } from "vue";
import { useContext } from "#imports";

export default function({ trackLoading = true } = {}) {
  const context = useContext();

  const page = context.injectPageContext();
  const pending = context.injectPagePending();
  const loading = trackLoading ? context.injectLoading() : ref(false);

  function get(key) {
    return page[key];
  }

  // NOTE while the writer is loading, value is not known yet, so key is marked
  // as pending for reactions to wait until it is written
  function set(key, value) {
    if(loading.value) {
      pending.add(key);

      return;
    }

    page[key] = value;
    pending.delete(key);
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