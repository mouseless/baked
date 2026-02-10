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
import { computed } from "vue";
import { Button } from "primevue";
import { useDataMounter, usePathBuilder } from "#imports";
import { AwaitLoading } from "#components";

const { mount: mountData } = useDataMounter();
const pathBuilder = usePathBuilder();

const { schema, data } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, required: true }
});

const { icon, path, query: queryData, params: paramsData } = schema;

const query = mountData(queryData);
const params = mountData(paramsData);

const to = computed(() => ({
  path: params.value ? pathBuilder.build(path, params.value, { forRoute: true }) : path,
  query: query.value
}));
</script>
