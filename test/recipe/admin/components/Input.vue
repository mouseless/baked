<template>
  <InputText
    v-model="model"
    :data-testid="schema"
  />
</template>
<script setup>
import { InputText } from "primevue";

defineProps({
  schema: { type: null, required: true }
});

const model = defineModel({
  type: null,
  required: true
});
</script>
