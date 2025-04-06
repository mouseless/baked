<template>
  <Skeleton
    v-if="loading"
    height="1.5rem"
  />
  <span v-else>{{ display }}</span>
</template>
<script setup>
import { computed } from "vue";
import { Skeleton } from "primevue";
import { useContext, useFormat } from "#imports";

const context = useContext();
const { asPercentage } = useFormat();

const { data } = defineProps({
  schema: { type: null, default: null },
  data: { type: null, required: true }
});

const loading = context.loading();
const display = computed(() => asPercentage(data));
</script>
