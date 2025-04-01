<template>
  <div
    v-if="loading"
    class="min-w-60"
  >
    <Skeleton class="min-h-10" />
  </div>
  <FloatLabel
    v-else
    variant="on"
  >
    <Select
      v-model="selected"
      :input-id="path"
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
import { defineAsyncComponent, ref, watch } from "vue";
const FloatLabel = defineAsyncComponent(() => import("primevue/floatlabel"));
const Select = defineAsyncComponent(() => import("primevue/select"));
const Skeleton = defineAsyncComponent(() => import("primevue/skeleton"));
import { useContext, useUiStates } from "#imports";

const { schema, data, loading } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, required: true },
  loading: { type: Boolean, default: false }
});

const model = defineModel({
  type: null,
  required: true
});

const { label, optionLabel, optionValue, showClear, stateful } = schema;

const context = useContext();
const { value: { selectStates } } = useUiStates();

const path = context.path();
const selected = ref();

if(stateful) {
  model.value = selectStates[path] || model.value;
}

if(!loading) {
  setSelected(model.value);
}

// two way binding between model and selected
watch(model, newModel => setSelected(newModel));
watch(selected, newSelected => setModel(newSelected));

function setModel(selected) {
  const selectedValue = optionValue ? selected?.[optionValue] : selected;
  model.value = selectedValue;

  if(stateful) {
    selectStates[path] = selectedValue;
  }
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
