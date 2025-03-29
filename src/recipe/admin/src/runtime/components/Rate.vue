<template>
  <Skeleton
    v-if="loading"
    height="1.5rem"
  />
  <span v-else>{{ display }}</span>
</template>
<script setup>
import { computed, defineAsyncComponent } from "vue";
const Skeleton = defineAsyncComponent(() => import("primevue/skeleton"));
import { useFormat } from "#imports";

const { data } = defineProps({
  schema: { type: null, default: null },
  data: { type: null, required: true },
  loading: { type: Boolean, default: false }
});

const { asPercentage } = useFormat();

const display = computed(() => asPercentage(data));
</script>
