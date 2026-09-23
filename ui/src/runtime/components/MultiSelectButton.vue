<template>
  <AwaitLoading
    :skeleton="{
      height: label.mode === 'ifta' ? '3.6rem' : '2.6rem',
      class: 'min-w-60'
    }"
  >
    <!-- NOTE - This component is a copy of SelectButton, consider refactoring to reuse when implementing in baked -->
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
          v-model="selected"
          :options="data"
          multiple
          :allow-empty
          :data-key="optionValue"
          :option-label
          :pt="{ pcToggleButton: { root: { class: 'text-[length:inherit]' } } }"
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

const { allowEmpty = false, label, localizeOptionLabels, optionLabel, optionValue, stateful, targetProp } = schema;

const path = context.injectPath();
const selected = ref([]);

// NOTE - Duplicates in Select, SelectButton, MultiSelect, and MultiSelectButton
// should be reduced two way binding between model and selected
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
  // TODO - This has not been tested! It will be tested with Locatables
  return targetProp ? model.value?.map(selection => selection[targetProp]) : model.value;
}

function setModel(selected) {
  const selectedValue = getSelectedValue(selected);
  const value = selectedValue?.length ? selectedValue : undefined;

  if(stateful) {
    selectButtonStates[path] = value;
  }

  const newModel = value
    ? targetProp
      ? value.map(v => ({ [targetProp]: v }))
      : value
    : undefined;
  if(arrayEquals(newModel, model.value)) { return; }

  model.value = newModel;
}

function setSelected(value) {
  // data can be null when data is async
  if(!data) { return; }

  selected.value = optionValue
    ? data.filter(o => value?.includes(o[optionValue]))
    : value;

  if(stateful) {
    const selectedValue = getSelectedValue(selected.value);
    if(getModel() !== selectedValue) {
      setModel(selected.value);
    }
  }
}

function getSelectedValue(selected) {
  return optionValue ? selected?.map(s => s[optionValue]) : selected;
}

function arrayEquals(a, b) {
  return a?.length === b?.length && a?.every((v, i) => v === b[i]);
}
</script>
