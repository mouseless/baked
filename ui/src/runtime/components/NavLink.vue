<template>
  <AwaitLoading :skeleton="{ height: '1.5rem' }">
    <Button
      as="router-link"
      link
      :label="l(text)"
      :to
    />
  </AwaitLoading>
</template>
<script setup>
import { computed } from "vue";
import { Button } from "primevue";
import { useFormat, useLocalization } from "#imports";
import { AwaitLoading } from "#components";

const { format } = useFormat();
const { localize: l } = useLocalization();

const { schema, data } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, required: true }
});

const { path, idProp, textProp } = schema;

// TODO: this format call is temporary, final design should handle path
// variables using name, not index, e.g., /test/{0} -> /test/{id}
const to = computed(() => format(path, [data[idProp]]));
const text = computed(() => data[textProp]);
</script>
