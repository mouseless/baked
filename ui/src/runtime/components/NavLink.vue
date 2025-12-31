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

const { icon, path, query: queryData, params: paramsData } = schema;

const contextData = context.injectContextData();
const query = ref(queryData ? dataFetcher.get({ data: queryData, contextData }) : null);
const shouldLoadQuery = queryData ? dataFetcher.shouldLoad(queryData.type) : false;
const params = ref(paramsData ? dataFetcher.get({ data: paramsData, contextData }) : null);
const shouldLoadParams = paramsData ? dataFetcher.shouldLoad(paramsData.type) : false;
const to = computed(() => ({
  path: params.value ? pathBuilder.build(path, params.value, { forRoute: true }) : path,
  query: query.value
}));

onMounted(async() => {
  if(shouldLoadQuery) {
    query.value = await dataFetcher.fetch({ data: queryData, contextData });
  }

  if(shouldLoadParams) {
    params.value = await dataFetcher.fetch({ data: paramsData, contextData });
  }
});
</script>
