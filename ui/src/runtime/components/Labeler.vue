<template>
  <template v-if="!text">
    <slot />
  </template>
  <component
    :is="labelComponent"
    v-else
    :dt
    :pt
    :variant
    :required
  >
    <template #default>
      <slot />
      <label :for="path">
        {{ localizedText }}
      </label>
    </template>
  </component>
</template>
<script setup>
import { computed } from "vue";
import { FloatLabel, IftaLabel } from "primevue";
import { useContext, useLocalization } from "#imports";

const context = useContext();
const { localize: l } = useLocalization({});
const { localize: lc } = useLocalization({ group: "Labeler" });

const { label } = defineProps({
  label: { type: Object, default: null },
  path: { type: String, required: true },
  pt: { type: Object, default: () => ({ }) },
  dt: { type: Object, default: () => ({ }) }
});

const { text, mode, variant = "on", showOptionality } = label ?? { };

const validation = context.injectValidation();
const required = computed(() => validation?.value?.required);
const localizedText = computed(() => {
  if(!showOptionality) {
    return l(text);
  }

  if(required.value) {
    return lc("{label} (Required)", { label: l(text) });
  }

  return lc("{label} (Optional)", { label: l(text) });
});
const labelComponent = computed(() => {
  if(mode === "ifta") { return IftaLabel; }
  if(mode === "float") { return FloatLabel; }

  throw new Error("`mode` must be set when `text` is set");
});
</script>