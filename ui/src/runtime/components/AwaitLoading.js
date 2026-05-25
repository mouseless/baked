import { h, useSlots } from "vue";
import { Skeleton } from "primevue";
import { InlineError } from "#components";
import { useBakeError, useContext, useUnref } from "#imports";

export default {
  props: {
    skeleton: { type: Object, default: () => { } },
    noError: { type: Boolean, default: false }
  },
  setup(props) {
    const { claim: claimError } = useBakeError();
    const context = useContext();
    const unref = useUnref();
    const slots = useSlots();

    const loading = context.injectLoading();

    const error = props.noError ? null : claimError();

    return () => {
      if(loading.value) {
        return render(slots.loading, {
          fallback: () => h(Skeleton, props.skeleton)
        });
      }

      if(!props.noError && error?.raw.value) {
        return render(slots.error, {
          props: { error: unref.deepUnref(error) },
          fallback: () => h(InlineError, { style: props.skeleton, error: unref.deepUnref(error) })
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