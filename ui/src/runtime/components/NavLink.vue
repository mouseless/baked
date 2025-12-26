<template>
  <AwaitLoading :skeleton="{ height: '1.5rem' }">
    <Button
      as="router-link"
      :icon
      link
      :label="data.name"
      :to
    />
  </AwaitLoading>
</template>
<script setup>
import { Button } from "primevue";
import { usePathBuilder } from "#imports";
import { AwaitLoading } from "#components";

const pathBuilder = usePathBuilder();

const { schema, data } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, required: true }
});

const { icon, path, params } = schema;

const to  = pathBuilder.build(path, data.params);
</script>
