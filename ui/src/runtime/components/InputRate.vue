<template>
  <AwaitLoading
    :skeleton="{
      height: label?.mode === 'ifta' ? '3.6rem' : '2.6rem',
      class: 'min-w-60'
    }"
  >
    <Validation>
      <Labeler
        :label
        :path
      >
        <InputNumber
          ref="number"
          v-model="inputModel"
          v-bind="$attrs"
          class="min-w-60"
          prefix="%"
          min="0"
          :max="MAX_VALUE"
          :min-fraction-digits="0"
          :max-fraction-digits="2"
          :disabled
          @input="onInput"
          @keydown="onKeydown"
        />
      </Labeler>
    </Validation>
  </AwaitLoading>
</template>
<script setup>
import { ref, watch } from "vue";
import { InputNumber } from "primevue";
import { useContext } from "#imports";
import { AwaitLoading, Labeler, Validation } from "#components";

const context = useContext();

const { schema } = defineProps({
  schema: { type: null, required: true }
});

const model = defineModel({ type: null, required: true });
const inputModel = ref();

const { label, disabled, max } = schema;

const MAX_VALUE = max || Number.MAX_SAFE_INTEGER;
const path = context.injectPath();

watch(
  () => model.value,
  newVal => {
    if(newVal != null) {
      inputModel.value = newVal * 100;
    }
  },
  { immediate: true }
);

function onInput(event) {
  if(event.value > MAX_VALUE) {
    inputModel.value = MAX_VALUE;
    return;
  }

  model.value = event.value / 100;
}

// AI-GEN (Claude - Haiku 4.5)
// prompt: create a keydown filter for max number input with key names in js
function onKeydown(event) {
  const ALLOWED_KEYS = [
    "Backspace",
    "Delete",
    "Tab",
    "Escape",
    "Enter",
    "ArrowLeft",
    "ArrowRight",
    "ArrowUp",
    "ArrowDown"
  ];

  if(event.ctrlKey || event.metaKey || ALLOWED_KEYS.includes(event.key)) {
    return;
  }

  const isDigit = /^[0-9]$/.test(event.key);
  if(!isDigit) {
    return;
  }

  const currentValue = inputModel.value ?? 0;

  if(currentValue >= MAX_VALUE) {
    event.preventDefault();
  }
}
</script>