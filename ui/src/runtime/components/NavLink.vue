<template>
  <AwaitLoading :skeleton="{ height: '1.5rem' }">
    <Button
      v-if="data"
      :icon
      :label
      :to
      as="router-link"
      link
      class="m-0 p-0"
    />
    <span v-else>-</span>
  </AwaitLoading>
</template>
<script setup>
import { computed } from "vue";
import { Button } from "primevue";
import { useDataMounter, usePathBuilder } from "#imports";
import { AwaitLoading } from "#components";

const { mount: mountData } = useDataMounter({ defaultInlineError: true });
const pathBuilder = usePathBuilder();

const { schema, data } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, required: true }
});

const { icon, labelProp, path, query: queryData, params: paramsData } = schema;

const query = mountData(queryData);
const params = mountData(paramsData);

const label = computed(() => labelProp ? data?.[labelProp] : data);
const to = computed(() => ({
  path: params.value ? pathBuilder.build(path, params.value, { forRoute: true }) : path,
  query: query.value
}));
</script>