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
          :option-value
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
import { Select } from "primevue";
import { useContext, useUiStates, useLocalization, useSelection } from "#imports";
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
const { selected } = useSelection({
  data,
  model,
  path,
  stateStore: selectStates,
  stateful,
  targetProp,
  mode: "single"
});

const placeholder = label?.text ? l(label.text) : null;

function getOptionLabel(slotProps) {
  const result = slotProps.option[optionLabel] ?? slotProps.option;

  return localizeOptionLabels ? l(result) : result;
}

function getValueLabel(slotProps) {
  const option = optionValue && slotProps.value != null
    ? data?.find(item => item[optionValue] === slotProps.value)
    : slotProps.value;
  const result = option?.[optionLabel] ?? option ?? placeholder;

  // return "\u00A0" to display full height
  return (localizeOptionLabels ? l(result) : result) ?? "\u00A0";
}

</script>