<template>
  <Skeleton
    v-if="loading"
    width="3rem"
    height="1.5rem"
  />
  <span v-else>{{ data }}</span>
</template>
<script setup>
import { Skeleton } from "primevue";

defineProps({
  schema: { type: null, default: null },
  data: { type: null, required: true },
  loading: { type: Boolean, default: false }
});
</script>
