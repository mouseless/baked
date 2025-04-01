<template>
  <div
    v-if="loading"
    class="min-w-60"
  >
    <Skeleton class="min-h-10" />
  </div>
  <SelectButton
    v-model="selected"
    :options="data"
    :allow-empty
    :option-label="optionLabel"
    :data-key="optionValue"
  />
</template>
<script setup>
import { defineAsyncComponent, ref, watch } from "vue";
const SelectButton = defineAsyncComponent(() => import("primevue/selectbutton"));
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

const { allowEmpty, optionLabel, optionValue, stateful } = schema;

const context = useContext();
const { value: { selectButtonStates } } = useUiStates();

const path = context.path();
const selected = ref();

if(stateful) {
  model.value = selectButtonStates[path] || model.value;
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
    selectButtonStates[path] = selectedValue;
  }
}

function setSelected(value) {
  selected.value = optionValue
    ? data.filter(o => o[optionValue] === value)[0]
    : value;
}
</script>
