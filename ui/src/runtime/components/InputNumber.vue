<template>
  <InputNumber
    v-model="model"
    @input="onInput"
  />
</template>
<script setup>
import { InputNumber } from "primevue";

defineProps({
  schema: { type: null, required: true }
});
const model = defineModel({ type: null, required: true });

function onInput(event) {
  model.value = event.value;
}
</script>
