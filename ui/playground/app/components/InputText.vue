<template>
  <InputText
    v-model="model"
    :name="testId"
    :data-testid="testId"
    :placeholder="testId"
    class="w-32"
  />
</template>
<script setup>
import { InputText } from "primevue";
import { watch } from "vue";

const { schema } = defineProps({
  schema: { type: null, required: true }
});
const model = defineModel({ type: null, required: true });

const { testId, defaultValue } = schema;

if(defaultValue !== undefined) {
  watch(
    model,
    newValue => {
      if(newValue == null) {
        model.value = defaultValue;
      }
    },
    { immediate: true }
  );
}
</script>
