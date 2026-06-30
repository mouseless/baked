<template>
  <AwaitLoading
    :skeleton="{
      height: label?.mode === 'ifta' ? '3.6rem' : '2.6rem',
      class: 'min-w-60'
    }"
  >
    <Validation>
      <Labeler
        :label="{
          ...label,
          mode: label?.mode === 'ifta' ? label.mode : null,
          text: label?.mode === 'ifta' ? label.text : null
        }"
        :path
        :dt="{
          colorScheme: {
            light: {
              top: '-1rem',
            },
            dark: {
              top: '-1rem'
            }
          }
        }"
      >
        <SelectButton
          v-if="data"
          v-bind="$attrs"
          v-model="selected"
          :options="data"
          :allow-empty
          :data-key="optionValue"
          :option-label
          class="!w-auto"
          pt:pc-toggle-button:root="text-[length:inherit]"
        >
          <template #option="slotProps">
            <span>{{ getOptionLabel(slotProps) }}</span>
          </template>
        </SelectButton>
      </Labeler>
    </Validation>
  </AwaitLoading>
</template>
<script setup>
import { ref, watch } from "vue";
import { SelectButton } from "primevue";
import { useContext, useLocalization, useUiStates } from "#imports";
import { AwaitLoading, Labeler, Validation } from "#components";

const context = useContext();
const { localize: l } = useLocalization();
const { value: { selectButtonStates } } = useUiStates();

const { schema, data } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, required: true }
});
const model = defineModel({ type: null, required: true });

const {
  allowEmpty = false,
  label,
  localizeOptionLabels,
  optionLabel,
  optionValue,
  stateful,
  targetProp
} = schema;

const path = context.injectPath();

const selected = ref();

watch(
  [() => data, getModel],
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

  return localizeOptionLabels ? l(result) : result;
}

function getModel() {
  return targetProp ? model.value?.[targetProp] : model.value;
}

function setModel(selected) {
  const selectedValue = optionValue ? selected?.[optionValue] : selected;
  if(stateful) {
    selectButtonStates[path] = selectedValue;
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