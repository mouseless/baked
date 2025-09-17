<template>
  <div
    v-if="loading"
    class="min-w-40"
  >
    <Skeleton class="min-h-10" />
  </div>
  <FloatLabel
    v-else-if="data"
    variant="on"
  >
    <Select
      v-model="selected"
      v-bind="$attrs"
      :input-id="path"
      :options="data"
      :placeholder="label"
      :show-clear
      class="hide-placeholder"
    >
      <template #value="slotProps">
        <span>
          {{ getValueLabel(slotProps) }}
        </span>
      </template>
      <template #option="slotProps">
        <span>{{ getOptionLabel(slotProps) }}</span>
      </template>
    </Select>
    <label for="period">{{ l(label) }}</label>
  </FloatLabel>
</template>
<script setup>
import { ref, watch } from "vue";
import { FloatLabel, Select, Skeleton } from "primevue";
import { useContext, useUiStates, useLocalization } from "#imports";

const context = useContext();
const { localize: l } = useLocalization({});
const { value: { selectStates } } = useUiStates();

const { schema, data } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, required: true }
});
const model = defineModel({ type: null, required: true });

const { label, localizeLabel, optionLabel, optionValue, showClear, selectionPageContextKey, stateful } = schema;

const loading = context.loading();
const path = context.path();
const page = context.injectPage();
const selected = ref();

// two way binding between model and selected
watch(
  [() => data, () => model.value],
  ([_data, _model]) => {
    if(!_data) { return; }
    const value = stateful ? (selectStates[path] ?? _model) : _model;
    setSelected(value);
  },
  { immediate: true }
);
watch(selected, newSelected => setModel(newSelected));

function getOptionLabel(slotProps) {
  const result = slotProps.option[optionLabel] ?? slotProps.option;

  return localizeLabel ? l(result) : result;
}

function getValueLabel(slotProps) {
  const result = slotProps.value?.[optionLabel] ?? slotProps.value ?? label;

  return localizeLabel ? l(result) : result;
}

function setModel(selected) {
  const selectedValue = optionValue ? selected?.[optionValue] : selected;
  model.value = selectedValue;

  if(stateful) {
    selectStates[path] = selectedValue;
  }
}

function getValueOf(option) {
  return optionValue ? option?.[optionValue] : option;
}

function setSelected(value) {
  // data can be null when data is async
  if(!data) { return; }

  selected.value = optionValue
    ? data.filter(o => o[optionValue] === value)[0]
    : value;

  if(selectionPageContextKey) {
    for(const currentValue of data.map(getValueOf)) {
      setPageContext(currentValue, false);
    }

    const selectedValue = getValueOf(selected.value);
    if(selectedValue !== undefined) {
      setPageContext(selectedValue, true);
    }
  }

  if(stateful) {
    const selectedValue = optionValue ? selected.value?.[optionValue] : selected.value;
    if(model.value !== selectedValue) {
      setModel(selected.value);
    }
  }
}

function setPageContext(key, value) {
  page[`${selectionPageContextKey}:${key}`] = value;
  page[`!${selectionPageContextKey}:${key}`] = !value;
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

.p-select-label {
  font-size: inherit;
}
</style>
