<template>
  <IconField>
    <InputIcon class="pi pi-search" />
    <InputText
      v-model="inputModel"
      autofocus
      :placeholder="placeholder ? l(placeholder) : undefined"
    />
  </IconField>
</template>
<script setup>
import { ref, watch } from "vue";
import { IconField, InputIcon, InputText } from "primevue";
import { useLocalization } from "#imports";

const { localize: l } = useLocalization();

const { schema } = defineProps({
  schema: { type: null, required: true }
});
const model = defineModel({ type: null, required: true });

const { placeholder, ignoreWhiteSpace } = schema;

const inputModel = ref(model.value);

watch(inputModel, value => model.value = ignoreWhiteSpace ? value.trim() : value);
</script>
