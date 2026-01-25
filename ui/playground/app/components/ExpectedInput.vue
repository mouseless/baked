<template>
  <InputNumber
    v-if="number"
    v-model="model"
    :name="testId"
    :data-testid="testId"
    :placeholder="testId"
    class="w-32"
    @input="onInput"
  />
  <InputText
    v-else
    v-model="model"
    :name="testId"
    :data-testid="testId"
    :placeholder="testId"
    class="w-32"
  />
</template>
<script setup>
import { watch } from "vue";
import { InputNumber, InputText } from "primevue";

const { schema } = defineProps({
  schema: { type: null, required: true }
});
const model = defineModel({ type: null, required: true });

const { testId, defaultValue, number } = schema;

watch(model, newValue => {
  if(newValue !== undefined && newValue !== null) { return; }
  if(newValue === defaultValue) { return; }

  model.value = defaultValue;
}, { immediate: true });

function onInput(event) {
  model.value = event.value;
}
</script>
