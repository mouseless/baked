<template>
  <Skeleton
    v-if="loading"
    height="1.5rem"
  />
  <Button
    v-else-if="data"
    as="router-link"
    link
    :label="l(text)"
    :to
  />
</template>
<script setup>
import { computed } from "vue";
import { Button, Skeleton } from "primevue";
import { useContext, useFormat, useLocalization } from "#imports";

const context = useContext();
const { format } = useFormat();
const { localize: l } = useLocalization();

const { schema, data } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, required: true }
});

const { path, idProp, textProp } = schema;

const loading = context.injectLoading();
// TODO: this format call is temporary, final design should handle path
// variables using name, not index, e.g., /test/{0} -> /test/{id}
const to = computed(() => format(path, [data[idProp]]));
const text = computed(() => data[textProp]);
</script>
