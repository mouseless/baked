<template>
  <IconField>
    <InputIcon class="pi pi-search" />
    <InputText
      v-model="page[pageContextKey]"
      autofocus
      :placeholder="placeholder ? l(placeholder) : undefined"
    />
  </IconField>
</template>
<script setup>
import { IconField, InputIcon, InputText } from "primevue";
import { useContext, useLocalization } from "#imports";

const context = useContext();
const { localize: l } = useLocalization();

const { schema } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, required: true }
});

const { placeholder, pageContextKey } = schema;

const page = context.page();
</script>
