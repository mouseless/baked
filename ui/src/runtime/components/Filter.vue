<template>
  <IconField>
    <InputIcon class="pi pi-search" />
    <InputText
      v-model="model"
      autofocus
      :placeholder="placeholder ? l(placeholder) : undefined"
    />
  </IconField>
</template>
<script setup>
import { IconField, InputIcon, InputText } from "primevue";
import { useLocalization } from "#imports";

const { localize: l } = useLocalization();

const { schema } = defineProps({
  schema: { type: null, required: true }
});
defineModel({ type: null, required: true });

const { placeholder } = schema;
</script>
