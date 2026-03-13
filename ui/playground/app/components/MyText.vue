<template>
  <div class="flex gap-2 items-baseline">
    <Text
      v-bind="$attrs"
      :schema
      :data
    />
    <Tag
      v-if="somethingExtra"
      severity="info"
    >
      {{ somethingExtra }}
    </Tag>
  </div>
</template>
<script setup>
import { Tag } from "primevue";
import { Text } from "#components";

const { schema, data } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, required: true }
});

const { somethingExtra } = schema;
</script>
