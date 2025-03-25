<template>
  <div
    v-if="loading"
    class="min-w-60"
  >
    <Skeleton class="min-h-10" />
  </div>
  <FloatLabel v-else>
    <Select
      v-model="selected"
      :input-id="uiContext"
      :options="data"
      :option-label="optionLabel"
      :placeholder="label"
      :show-clear
      class="hide-placeholder"
    />
    <label for="period">{{ label }}</label>
  </FloatLabel>
</template>
<script setup>
import { inject, ref, watch } from "vue";
import { FloatLabel, Select, Skeleton } from "primevue";

const { schema, data, loading } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, required: true },
  loading: { type: Boolean, default: false }
});

const model = defineModel({
  type: null,
  required: true
});

const { label, optionLabel, optionValue, showClear } = schema;

const uiContext = inject("uiContext");

const selected = ref({});
watch(selected, newSelected => setModel(newSelected));

if(!loading) { setSelected(model.value); }
watch(model, newModel => setSelected(newModel));
watch(() => loading, () => setSelected(model.value));

function setModel(selected) {
  model.value = optionValue ? selected?.[optionValue] : selected;
}

function setSelected(value) {
  selected.value = optionValue
    ? data.filter(o => o[optionValue] === value)[0]
    : value;
}
</script>
<style lang="scss">
/*
placeholder gives select the initial width, but it overlaps with label so it is
hidden
*/
.hide-placeholder .p-placeholder {
  visibility: hidden;
}
</style>
