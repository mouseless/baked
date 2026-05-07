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
        {{ l(label) }}
      </label>
    </template>
  </component>
</template>

<script setup>
import { computed } from "vue";
import { FloatLabel, IftaLabel } from "primevue";
import { useLocalization } from "#imports";

const { localize: l } = useLocalization();

const { label, mode } = defineProps({
  label: { type: String, required: true },
  path: { type: String, required: true },
  mode: { type: String, default: null },
  variant: { type: String, default: "on" },
  pt: { type: Object, default: () => { } },
  dt: { type: Object, default: () => { } }
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