import { h, useSlots } from "vue";
import { Skeleton } from "primevue";
import { useContext } from "#imports";

export default {
  props: {
    skeleton: { type: Object, required: false, default: () => { } }
  },
  setup() {
    const context = useContext();
    const slots = useSlots();

    const loading = context.injectLoading();

    return props => {
      if(loading.value) {
        if(slots.loading) {
          return slots.loading()[0];
        }

        return h(Skeleton, props.skeleton);
      }

      return slots.default()[0];
    };
  }
};