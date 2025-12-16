<template>
  <Bake
    v-model="model"
    :name="input.name"
    :descriptor="input.component"
  />
</template>
<script setup>
import { onMounted, watch } from "vue";
import { Bake } from "#components";
import { useContext, useDataFetcher } from "#imports";

const context = useContext();
const dataFetcher = useDataFetcher();

const { input } = defineProps({
  input: { type: Object, required: true }
});
const model = defineModel({ type: null, required: true });

const contextData = context.injectContextData();
let defaultValue = dataFetcher.get({ data: input.default, contextData });

onMounted(async() => {
  if(dataFetcher.shouldLoad(input.default?.type)) {
    defaultValue = await dataFetcher.fetch({ data: input.default, contextData });
  }

  model.value = defaultValue;
});

watch(model, newValue => {
  if(!checkValue(newValue) && input.required) {
    model.value = defaultValue;
  }
});

function checkValue(value) {
  if(typeof value === "string") {
    return value !== "";
  }

  return value !== undefined && value !== null;
}
</script>
