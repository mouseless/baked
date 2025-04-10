<template>
  <IconField>
    <InputIcon class="pi pi-search" />
    <InputText
      v-model="page[pageContextKey]"
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

const { placeholder, pageContextKey } = schema;
const page = context.page();
</script>
