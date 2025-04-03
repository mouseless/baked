<template>
  <Skeleton
    v-if="loading"
    height="1.5rem"
  />
  <span
    v-else
    v-tooltip.bottom="tooltip"
  >{{ display }}</span>
</template>
<script setup>
import { computed } from "vue";
import { Skeleton } from "primevue";
import { useFormat } from "#imports";

const { data } = defineProps({
  schema: { type: null, default: null },
  data: { type: null, required: true },
  loading: { type: Boolean, default: false }
});

const { asCurrency } = useFormat();

const display = computed(() => asCurrency(data));
const tooltip = computed(() => display.value.shortened ? `${asCurrency(data, { shorten: false })}` : null);
</script>
