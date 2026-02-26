<template>
  <AwaitLoading>
    <template #loading>
      <div class="min-w-40">
        <Skeleton class="min-h-10" />
      </div>
    </template>
    <FloatLabel variant="on">
      <Select
        v-bind="$attrs"
        v-model="selected"
        :input-id="path"
        :options="data"
        :placeholder="l(label)"
        :show-clear
        :filter
        class="hide-placeholder w-full"
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
      <label :for="path">{{ l(label) }}</label>
    </FloatLabel>
  </AwaitLoading>
</template>
<script setup>
import { ref, watch } from "vue";
import { FloatLabel, Select, Skeleton } from "primevue";
import { useContext, useUiStates, useLocalization } from "#imports";
import { AwaitLoading } from "#components";

const context = useContext();
const { localize: l } = useLocalization();
const { value: { selectStates } } = useUiStates();

const { schema, data } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, required: true }
});
const model = defineModel({ type: null, required: true });

const { filter, label, localizeLabel, optionLabel, optionValue, showClear, stateful, targetProp } = schema;

const path = context.injectPath();
const selected = ref();

// two way binding between model and selected
watch(
  [() => data, () => targetProp ? model.value[targetProp] : model.value],
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
  const newModel = targetProp ? { [targetProp]: selectedValue } : selectedValue;
  model.value = newModel;

  if(stateful) {
    selectStates[path] = newModel;
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
<style>
.b-component--Select {
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
}
</style>
