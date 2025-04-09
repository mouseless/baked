<template>
  <IconField>
    <InputIcon class="pi pi-search" />
    <InputText
      v-model="filteringKey"
      :placeholder
    />
  </IconField>
</template>
<script setup>
import { ref } from "vue";
import { IconField, InputIcon, InputText } from "primevue";
import { useContext } from "#imports";

const context = useContext();

const { schema, data } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, required: true }
});

const filteringKey = ref("");

const page = context.page();
page[schema.contextKey] = filteringKey;

const { placeholder } = schema;
</script>
