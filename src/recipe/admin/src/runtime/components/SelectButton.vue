<template>
  <div
    v-if="loading"
    class="min-w-60"
  >
    <Skeleton class="min-h-10" />
  </div>
  <SelectButton
    v-else-if="data"
    v-model="selected"
    :options="data"
    :allow-empty
    :data-key="optionValue"
    :pt="{ pcToggleButton: { root: { class: 'text-[length:inherit]' } } }"
  >
    <template #option="slotProps">
      <span>{{ getOptionLabel(slotProps) }}</span>
    </template>
  </SelectButton>
</template>
<script setup>
import { ref, watch } from "vue";
import { SelectButton, Skeleton } from "primevue";
import { useContext, useLocalization, useUiStates } from "#imports";

const context = useContext();
const { localize: l } = useLocalization();
const { value: { selectButtonStates } } = useUiStates();

const { schema, data } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, required: true }
});
const model = defineModel({ type: null, required: true });

const { allowEmpty, localizeLabel, optionLabel, optionValue, stateful } = schema;

const loading = context.loading();
const path = context.path();
const selected = ref();

watch(
  [() => data, () => model.value],
  ([_data, _model]) => {
    if(!_data) { return; }
    const value = stateful ? (selectButtonStates[path] ?? _model) : _model;
    setSelected(value);
  },
  { immediate: true }
);
watch(selected, newSelected => setModel(newSelected));

function getOptionLabel(slotProps) {
  const result = slotProps.option[optionLabel] ?? slotProps.option;

  return localizeLabel ? l(result) : result;
}

function setModel(selected) {
  const selectedValue = optionValue ? selected?.[optionValue] : selected;
  model.value = selectedValue;

  if(stateful) {
    selectButtonStates[path] = selectedValue;
  }
}

function setSelected(value) {
  // data can be null when data is async
  if(!data) { return; }

  selected.value = optionValue
    ? data.filter(o => o[optionValue] === value)[0]
    : value;

  if(stateful) {
    const selectedValue = optionValue ? selected.value?.[optionValue] : selected.value;
    if(model.value !== selectedValue) {
      setModel(selected.value);
    }
  }
}
</script>
