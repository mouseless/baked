<template>
  <Skeleton
    v-if="loading"
    height="1.5rem"
  />
  <span v-else-if="data">{{ data }}</span>
  <span v-else>-</span>
</template>
<script setup>
import { Skeleton } from "primevue";
import { useContext } from "#imports";

const context = useContext();

defineProps({
  schema: { type: null, default: null },
  data: { type: null, required: true }
});

const loading = context.loading();
</script>
