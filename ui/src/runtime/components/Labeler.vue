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
  >
    <template #default>
      <slot />
      <label
        class="max-sm:truncate max-sm:w-5/6"
        :for="path"
      >
        {{ localizedText }}
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

const { label, required } = defineProps({
  label: { type: Object, default: null },
  path: { type: String, required: true },
  pt: { type: Object, default: () => ({ }) },
  dt: { type: Object, default: () => ({ }) },
  required: { type: Boolean, default: false }
});

const { text, mode, variant = "on", showOptionality } = label ?? { };

const localizedText = computed(() => {
  if(!showOptionality) {
    return l(text);
  }

  if(required) {
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