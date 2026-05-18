<template>
  <component
    :is="labelComponent"
    :dt
    :pt
    :variant
  >
    <template #default>
      <slot />
      <label
        v-if="labelComponent !== 'div'"
        class="max-sm:truncate max-sm:w-5/6"
        :for="path"
      >
        {{ localizeLabel }}
      </label>
    </template>
  </component>
</template>
<script setup>
import { computed } from "vue";
import { FloatLabel, IftaLabel } from "primevue";
import { useLocalization } from "#imports";

const { localize: l } = useLocalization({});
const { localize: lc } = useLocalization({ group: "Labeler" });

const { label, mode, required, validateLabel } = defineProps({
  label: { type: String, required: true },
  path: { type: String, required: true },
  mode: { type: String, default: null },
  variant: { type: String, default: "on" },
  pt: { type: Object, default: () => { } },
  dt: { type: Object, default: () => { } },
  required: { type: Boolean, default: false },
  validateLabel: { type: Boolean, default: false }
});

const localizeLabel = computed(() => {
  if(!validateLabel) {
    return l(label);
  }

  if(required) {
    return lc("{label}_Required", { label: l(label) });
  }

  return lc("{label}_Optional", { label: l(label) });
});
const labelComponent = computed(() => {
  if(!label) { return "div"; }

  switch (mode) {
  case "ifta":
    return IftaLabel;
  case "float":
    return FloatLabel;
  default:
    return "div";
  }
});
</script>