<template>
  <AwaitLoading :skeleton="{ height: '1.5rem' }">
    <Button
      as="router-link"
      :icon
      link
      :label="l(text)"
      :to
    />
  </AwaitLoading>
</template>
<script setup>
import { computed } from "vue";
import { Button } from "primevue";
import { useLocalization, usePathBuilder } from "#imports";
import { AwaitLoading } from "#components";

const { localize: l } = useLocalization();
const pathBuilder = usePathBuilder();

const { schema, data } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, required: true }
});

const { icon, path, idProp, textProp } = schema;

const to = computed(() => pathBuilder.build(path, { [idProp]: data[idProp] }));
const text = computed(() => data[textProp]);
</script>
