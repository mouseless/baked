<template>
  <InputText
    v-model="model"
    :data-testid="testId"
    :placeholder="testId"
    class="w-32"
  />
</template>
<script setup>
import { InputText } from "primevue";
import { onMounted, watch } from "vue";

const { schema } = defineProps({
  schema: { type: null, required: true }
});
const model = defineModel({ type: null, required: true });

const { testId, defaultValue } = schema;

if(defaultValue) {
  onMounted(() => {
    if(!model.value) {
      model.value = defaultValue;
    }
  });

  watch(model, newValue => {
    if(!newValue) {
      model.value = defaultValue;
    }
  });
}
</script>
