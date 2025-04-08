<template>
  <IconField>
    <InputIcon class="pi pi-search" />
    <InputText
      v-model="searched"
      :placeholder
    />
  </IconField>
</template>
<script setup>
import { ref } from "vue";
import { IconField, InputIcon, InputText } from "primevue";

const { schema, data } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, required: true }
});

const searched = ref("");

const { placeholder } = schema;
</script>
