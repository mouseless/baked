import { AwaitLoading } from "#components";
import { h, useSlots } from "vue";

export default {
  setup() {
    const slots = useSlots();
    return () => h(AwaitLoading, null, {
      default: () => [h("div", { class: "flex flex-col" }, slots.default?.())],
      loading: slots.loading
    });
  }
};