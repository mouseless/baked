<template>
  <Skeleton
    v-if="loading"
    height="1.5rem"
  />
  <span
    v-else-if="data"
    v-tooltip.bottom="tooltip"
  >{{ text }}</span>
  <span v-else>-</span>
</template>
<script setup>
import { computed } from "vue";
import { Skeleton } from "primevue";
import { useContext, useFormat } from "#imports";

const context = useContext();
const { truncate } = useFormat();

const { schema, data } = defineProps({
  schema: { type: null, default: null },
  data: { type: null, required: true }
});

const loading = context.loading();
const lengthIsExceeded = computed(() => schema.maxLength && data.length > schema.maxLength);
const text = computed(() => lengthIsExceeded.value ? truncate(data, schema.maxLength): data);
const tooltip = computed(() => ({
  value: data,
  disabled: !lengthIsExceeded.value
}));
</script>
