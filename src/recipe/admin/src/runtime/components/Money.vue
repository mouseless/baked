<template>
  <Skeleton
    v-if="loading"
    height="1.5rem"
  />
  <span
    v-else-if="data"
    v-tooltip.bottom="tooltip"
  >{{ display }}</span>
  <span v-else>-</span>
</template>
<script setup>
import { computed } from "vue";
import { Skeleton } from "primevue";
import { useContext, useFormat } from "#imports";

const context = useContext();
const { asCurrency } = useFormat();

const { data } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, required: true }
});

const loading = context.injectLoading();
const display = computed(() => asCurrency(data));
const tooltip = computed(() => display.value.shortened ? `${asCurrency(data, { shorten: false })}` : null);
</script>
