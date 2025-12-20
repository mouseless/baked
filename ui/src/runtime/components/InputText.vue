<template>
  <InputText
    v-model="model"
  />
</template>
<script setup>
import { InputText } from "primevue";

defineProps({
  schema: { type: null, required: true }
});
defineModel({ type: null, required: true });
</script>
