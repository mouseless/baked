import { h, unref, useSlots } from "vue";
import { Skeleton } from "primevue";
import { LoadingError } from "#components";
import { useBakeError, useContext } from "#imports";

export default {
  props: {
    skeleton: { type: Object, default: () => { } },
    noError: { type: Boolean, default: false }
  },
  setup(props) {
    const { handle: handleError } = useBakeError();
    const context = useContext();
    const slots = useSlots();

    const loading = context.injectLoading();

    const { rawError, error } = props.noError ? { } : handleError();

    return () => {
      if(loading.value) {
        return render(slots.loading, {
          fallback: () => h(Skeleton, props.skeleton)
        });
      }

      if(rawError?.value) {
        return render(slots.error, {
          props: {
            error: unref(error),
            rawError: unref(rawError)
          },
          fallback: () => h(LoadingError, {
            style: props.skeleton,
            error: unref(error)
          })
        });
      }

      return render(slots.default);
    };
  }
};

function render(slot, { fallback, props } = {}) {
  if(!slot) {
    return fallback ? fallback() : null;
  }

  const children = slot(props);

  return children.length > 1
    ? h("div", children)
    : children[0];
}