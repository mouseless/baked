<template>
  <Skeleton
    v-if="loading"
    height="1.5rem"
  />
  <Button
    v-else
    as="router-link"
    link
    :label="text"
    :to
  />
</template>
<script setup>
import { computed } from "vue";
import { Button, Skeleton } from "primevue";
import { useDataFetcher } from "#imports";

const { schema, data } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, required: true },
  loading: { type: Boolean, default: false }
});

const dataFetcher = useDataFetcher();

const { path, idProp, textProp } = schema;

// TODO: this path and format call is temporary, final design should handle
// path variables using name, not index, e.g., /test/{0} -> /test/{id}
const to = computed(() => dataFetcher.format(path, [data[idProp]]));
const text = computed(() => data[textProp]);
</script>
