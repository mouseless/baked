<template>
  <AwaitLoading :skeleton="{ height: '1.5em', width: '10em' }">
    <span :data-testid="testId">{{ value }}</span>
  </AwaitLoading>
</template>
<script setup>
import { onMounted, ref } from "vue";
import { useContext, useDataFetcher } from "#imports";
import { AwaitLoading } from "#components";

const context = useContext();
const dataFetcher = useDataFetcher();

const { data, schema } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, required: true }
});

const { testId, showDataParams } = schema;

const dataDescriptor = context.injectDataDescriptor();
const contextData = context.injectContextData();

const value = ref(!showDataParams ? data : null);

onMounted(async() => {
  if(showDataParams) {
    value.value = await dataFetcher.fetchParameters({
      data: dataDescriptor,
      contextData
    });
  }
});
</script>
