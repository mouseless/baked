<template>
  <span :data-testid="testId">{{ value }}</span>
</template>
<script setup>
import { onMounted, ref } from "vue";
import { useContext, useDataFetcher } from "#imports";

const context = useContext();
const dataFetcher = useDataFetcher();

const { data, schema } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, required: true }
});

console.log(data);
const { testId, showDataParams } = schema;

const dataDescriptor = context.injectDataDescriptor();
const parentContext = context.injectParentContext();

const value = ref(!showDataParams ? data : null);

onMounted(async() => {
  if(showDataParams) {
    value.value = await dataFetcher.fetchParameters({
      data: dataDescriptor,
      contextData: { parent: parentContext }
    });
  }
});
</script>
