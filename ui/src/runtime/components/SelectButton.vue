<template>
  <AwaitLoading>
    <template #loading>
      <div class="min-w-60">
        <Skeleton class="min-h-10" />
      </div>
    </template>
    <Labeler
      :label="{
        ...label,
        mode: label?.mode == 'ifta' ? label.mode : null
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
        :pt="{ pcToggleButton: { root: { class: 'text-[length:inherit]' } } }"
        class="!w-auto"
      >
        <template #option="slotProps">
          <span>{{ getOptionLabel(slotProps) }}</span>
        </template>
      </SelectButton>
      <template #message>
        <Message
          v-if="validation"
          v-show="validation.message && validation.persist"
          :severity="validation.severity"
          variant="simple"
          size="small"
          class="ml-2"
        >
          {{ validation.message || "" }}
        </Message>
      </template>
    </Labeler>
  </AwaitLoading>
</template>
<script setup>
import { ref, watch } from "vue";
import { Message, SelectButton, Skeleton } from "primevue";
import { useContext, useLocalization, useUiStates } from "#imports";
import { AwaitLoading, Labeler } from "#components";

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
const validation = context.injectValidations();

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
<style>
.p-togglebutton-content {
  @apply whitespace-nowrap;
}

.p-popover-content {
  .b-component--SelectButton .p-selectbutton {
    @apply max-sm:flex max-sm:flex-col;

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
<style scoped>
.b-component--SelectButton {
  &:has(.p-iftalabel) {
    @apply mt-4;
  }
}

</style>