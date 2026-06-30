<template>
  <AwaitLoading
    :skeleton="{
      height: label?.mode === 'ifta' ? '3.6rem' : '2.6rem',
      class: 'min-w-40'
    }"
  >
    <Validation>
      <Labeler
        :label
        :path
      >
        <Select
          v-bind="$attrs"
          v-model="selected"
          :input-id="path"
          :options="data"
          :placeholder
          :show-clear
          :filter
          :auto-filter-focus="filter"
          :filter-fields="[optionLabel]"
          reset-filter-on-hide
        >
          <template #value="slotProps">
            <span>{{ getValueLabel(slotProps) }}</span>
          </template>
          <template #option="slotProps">
            <span>{{ getOptionLabel(slotProps) }}</span>
          </template>
        </Select>
      </Labeler>
    </Validation>
  </AwaitLoading>
</template>
<script setup>
import { ref, watch } from "vue";
import { Select } from "primevue";
import { useContext, useUiStates, useLocalization } from "#imports";
import { AwaitLoading, Labeler, Validation } from "#components";

const context = useContext();
const { localize: l } = useLocalization();
const { value: { selectStates } } = useUiStates();

const { schema, data } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, required: true }
});
const model = defineModel({ type: null, required: true });

const { filter, label, localizeOptionLabels, optionLabel, optionValue, showClear, stateful, targetProp } = schema;

const path = context.injectPath();

const selected = ref();
const placeholder = label?.text ? l(label.text) : null;

// two way binding between model and selected
watch(
  [() => data, getModel],
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

  return localizeOptionLabels ? l(result) : result;
}

function getValueLabel(slotProps) {
  const result = slotProps.value?.[optionLabel] ?? slotProps.value ?? placeholder;

  // return "\u00A0" to display full height
  return (localizeOptionLabels ? l(result) : result) ?? "\u00A0";
}

function getModel() {
  return targetProp ? model.value?.[targetProp] : model.value;
}

function setModel(selected) {
  const selectedValue = optionValue ? selected?.[optionValue] : selected;
  if(stateful) {
    selectStates[path] = selectedValue;
  }

  model.value = selectedValue
    ? targetProp
      ? { [targetProp]: selectedValue }
      : selectedValue
    : undefined;
}

function setSelected(value) {
  // data can be null when data is async
  if(!data) { return; }

  selected.value = optionValue
    ? data.filter(o => o[optionValue] === value)[0]
    : value;

  if(stateful) {
    const selectedValue = optionValue ? selected.value?.[optionValue] : selected.value;
    if(getModel() !== selectedValue) {
      setModel(selected.value);
    }
  }
}
</script>