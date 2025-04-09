<template>
  <IconField>
    <InputIcon class="pi pi-search" />
    <InputText
      v-model="page[contextKey]"
      :placeholder
    />
  </IconField>
</template>
<script setup>
import { IconField, InputIcon, InputText } from "primevue";
import { useContext } from "#imports";

const context = useContext();

const { schema, data } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, required: true }
});

const { placeholder, contextKey } = schema;
const page = context.page();
</script>
