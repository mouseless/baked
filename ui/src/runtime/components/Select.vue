<template>
  <AwaitLoading>
    <template #loading>
      <div class="min-w-40">
        <Skeleton class="min-h-10" />
      </div>
    </template>
    <Labeler
      :label
      :path
      :mode="labelMode"
      :variant="labelVariant"
      :validate-label
    >
      <Select
        v-bind="$attrs"
        v-model="selected"
        :input-id="path"
        :options="data"
        :placeholder="l(label)"
        :show-clear
        :filter
        :auto-filter-focus="filter"
        :filter-fields="[optionLabel]"
        reset-filter-on-hide
        class="w-full"
      >
        <template #value="slotProps">
          <span>{{ getValueLabel(slotProps) }}</span>
        </template>
        <template #option="slotProps">
          <span>{{ getOptionLabel(slotProps) }}</span>
        </template>
      </Select>
      <Message
        v-show="validator[name]?.message && validator[name]?.persist"
        :severity="validator[name]?.severity"
        variant="simple"
        size="small"
      >
        {{ validator[name]?.message || "" }}
      </Message>
    </Labeler>
  </AwaitLoading>
</template>
<script setup>
import { ref, watch } from "vue";
import { Message, Select, Skeleton } from "primevue";
import { useContext, useUiStates, useLocalization } from "#imports";
import { AwaitLoading, Labeler } from "#components";

const context = useContext();
const { localize: l } = useLocalization();
const { value: { selectStates } } = useUiStates();

const { schema, data } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, required: true }
});
const model = defineModel({ type: null, required: true });

const { filter, label, labelMode, validateLabel, labelVariant, localizeLabel, optionLabel, optionValue, showClear, stateful, targetProp } = schema;

const path = context.injectPath();
const { validator = {}, name } = context.injectParentContext();
const selected = ref();

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

  return localizeLabel ? l(result) : result;
}

function getValueLabel(slotProps) {
  const result = slotProps.value?.[optionLabel] ?? slotProps.value ?? label;

  // return "\u00A0" to display full height
  return (localizeLabel ? l(result) : result) ?? "\u00A0";
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
<style>
.b-component--Select {
  /*
  placeholder gives select the initial width, but it overlaps with label and
  tab key skip select if placeholder is hidden so it is opacity zero
  */
  .p-placeholder {
    opacity: 0;
  }

  .p-select-label {
    font-size: inherit;
  }
}
</style>
