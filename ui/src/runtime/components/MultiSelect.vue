<template>
  <AwaitLoading
    :skeleton="{
      height: label.mode === 'ifta' ? '3.6rem' : '2.6rem',
      class: 'min-w-40'
    }"
  >
    <Validation>
      <Labeler
        :label
        :path
      >
        <MultiSelect
          v-bind="$attrs"
          v-model="selected"
          :input-id="path"
          :options="data"
          :option-label="optionLabel"
          :option-value="optionValue"
          :placeholder
          :show-clear
          :filter
          :auto-filter-focus="filter"
          :filter-fields="[optionLabel]"
          :show-toggle-all
          reset-filter-on-hide
          class="w-full"
          :pt="{
            hiddenInput: { placeholder: null }
          }"
        >
          <template #value="{ value }">
            <template v-if="value?.length">
              <template v-if="value.length <= maxSelectedLabels">
                {{ getValueLabel(value) }}
              </template>
              <span v-else>{{ lc("{number} items selected", { number: value.length }) }}</span>
            </template>
          </template>
          <template #option="slotProps">
            <span>
              {{ getOptionLabel(slotProps) }}
            </span>
          </template>
        </MultiSelect>
      </Labeler>
    </Validation>
  </AwaitLoading>
</template>
<script setup>
import { ref, watch } from "vue";
import { MultiSelect } from "primevue";
import { useContext, useLocalization, useUiStates } from "#imports";
import { AwaitLoading, Labeler, Validation } from "#components";

const context = useContext();
const { localize: l } = useLocalization();
const { localize: lc } = useLocalization({ group: "MultiSelect" });
const { value: { selectStates } } = useUiStates();

const { schema, data } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, required: true }
});
const model = defineModel({ type: null, required: true });

const {
  maxSelectedLabels = 3,
  showToggleAll = false,
  filter,
  label,
  localizeOptionLabels,
  optionLabel,
  optionValue,
  showClear,
  stateful,
  targetProp
} = schema;

const path = context.injectPath();
const selected = ref();
const placeholder = label?.text ? l(label.text) : null;

// NOTE - Duplicates in Select, SelectButton, MultiSelect, and MultiSelectButton
// should be reduced two way binding between model and selected
watch(
  [() => data, getModel],
  ([_data, _model]) => {
    if(!_data) { return; }

    const value = stateful ? (selectStates[path] ?? _model) : _model;
    selected.value = value ?? null;
  },
  { immediate: true }
);
watch(selected, newSelected => setModel(newSelected));

function getOptionLabel(slotProps) {
  const result = slotProps.option[optionLabel] ?? slotProps.option;

  return localizeOptionLabels ? l(result) : result;
}

function getValueLabel(value) {
  return value
    .map(findLabel)
    .map(label => localizeOptionLabels ? l(label) : label)
    .join(", ");
}

function getModel() {
  // TODO - This has not been tested! It will be tested with Locatables
  return targetProp ? model.value?.map(selection => selection[targetProp]) : model.value;
}

function setModel(newSelected) {
  const value = newSelected?.length ? newSelected : undefined;

  if(stateful) {
    selectStates[path] = value;
  }

  model.value = value
    ? targetProp
      ? value.map(v => ({ [targetProp]: v }))
      : value
    : undefined;
}

function findLabel(value) {
  const option = optionValue
    ? data?.find(o => o[optionValue] === value)
    : data?.find(o => o === value);

  return optionLabel ? option?.[optionLabel] : option;
}
</script>