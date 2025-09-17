<template>
  <Skeleton
    v-if="loading"
    height="1.5rem"
  />
  <span v-else-if="data">{{ display }}</span>
  <span v-else>-</span>
</template>
<script setup>
import { computed } from "vue";
import { Skeleton } from "primevue";
import { useContext, useFormat } from "#imports";

const context = useContext();
const { asPercentage } = useFormat();

const { data } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, required: true }
});

const loading = context.injectLoading();
const display = computed(() => asPercentage(data));
</script>
