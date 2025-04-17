<template>
  <Skeleton
    v-if="loading"
    height="1.5rem"
  />
  <Message
    v-else-if="data"
    :severity="severity"
  >
    <template
      v-if="icon"
      #icon
    >
      <i :class="`pi ${icon}`" />
    </template>
    <span>{{ data }}</span>
  </Message>
</template>
<script setup>
import { Message, Skeleton } from "primevue";
import { useContext } from "#imports";

const context = useContext();

const { schema } = defineProps({
  schema: { type: null, default: null },
  data: { type: null, default: null }
});

const loading = context.loading();
const { icon, severity } = schema;
</script>
