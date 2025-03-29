<template>
  <Skeleton
    v-if="loading"
    height="1.5rem"
  />
  <span v-else>{{ data }}</span>
</template>
<script setup>
import { defineAsyncComponent } from "vue";
const Skeleton = defineAsyncComponent(() => import("primevue/skeleton"));

defineProps({
  schema: { type: null, default: null },
  data: { type: null, required: true },
  loading: { type: Boolean, default: false }
});
</script>
