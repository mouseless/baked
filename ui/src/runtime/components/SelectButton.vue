<template>
  <AwaitLoading>
    <template #loading>
      <div class="min-w-60">
        <Skeleton class="min-h-10" />
      </div>
    </template>
    <SelectButton
      v-if="data"
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
  </AwaitLoading>
</template>
<script setup>
import { ref, watch } from "vue";
import { SelectButton, Skeleton } from "primevue";
import { useContext, useLocalization, useUiStates } from "#imports";
import { AwaitLoading } from "#components";

const context = useContext();
const { localize: l } = useLocalization();
const { value: { selectButtonStates } } = useUiStates();

const { schema, data } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, required: true }
});
const model = defineModel({ type: null, required: true });

const { allowEmpty = false, localizeLabel, optionLabel, optionValue, stateful, selectionPageContextKey } = schema;

const path = context.injectPath();
const page = context.injectPage();
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
<style>
.p-popover-content {
  .p-selectbutton {
    @apply max-sm:flex-col;

    .p-togglebutton {
      &:first-child {
        @apply max-sm:rounded-t-lg max-sm:rounded-es-none;
      }
      &:last-child {
        @apply max-sm:rounded-b-lg max-sm:rounded-se-none;
      }
    }
  }
}
</style>