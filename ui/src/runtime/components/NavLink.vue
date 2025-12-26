<template>
  <AwaitLoading :skeleton="{ height: '1.5rem' }">
    <Button
      :icon
      :label="data"
      :to
      as="router-link"
      link
      class="m-0 p-0"
    />
  </AwaitLoading>
</template>
<script setup>
import { computed, onMounted, ref } from "vue";
import { Button } from "primevue";
import { useContext, useDataFetcher, usePathBuilder } from "#imports";
import { AwaitLoading } from "#components";

const context = useContext();
const dataFetcher = useDataFetcher();
const pathBuilder = usePathBuilder();

const { schema, data } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, required: true }
});

const { icon, path, query, params } = schema;

const contextData = context.injectContextData();
const queryData = ref();
const paramsData = ref();
const to = computed(() => {
  return {
    path: paramsData.value ? pathBuilder.build(path, paramsData.value) : path,
    query: queryData.value
  };
});

// this could have been `shouldLoad` but query and params can be null
// and `dataFetcher.shouldLoad` throws exception since no type prop exists
onMounted(async() => {
  if(query) {
    queryData.value = await dataFetcher.fetch({ data: query, contextData });
  }

  if(params) {
    paramsData.value = await dataFetcher.fetch({ data: params, contextData });
  }
});
</script>
