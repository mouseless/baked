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
import { watch } from "vue";
import { IconField, InputIcon, InputText } from "primevue";
import { useLocalization } from "#imports";

const { localize: l } = useLocalization();

const { schema } = defineProps({
  schema: { type: null, required: true }
});
const model = defineModel({ type: null, required: true });

const { placeholder, whiteSpaceSensitive } = schema;

watch(model, (newValue, oldValue) => {
  if(newValue === oldValue) { return; }
  if(whiteSpaceSensitive) { return; }

  model.value = newValue.trim();
});
</script>
