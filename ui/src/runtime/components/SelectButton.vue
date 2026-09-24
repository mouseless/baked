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
          :option-value
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
import { SelectButton } from "primevue";
import { useContext, useLocalization, useUiStates, useSelection } from "#imports";
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
const { selected } = useSelection({
  data,
  model,
  path,
  stateStore: selectButtonStates,
  stateful,
  targetProp,
  mode: "single"
});

function getOptionLabel(slotProps) {
  const result = slotProps.option[optionLabel] ?? slotProps.option;

  return localizeOptionLabels ? l(result) : result;
}

</script>