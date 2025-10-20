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
  schema: { type: null, required: true },
  data: { type: null, required: true }
});

const { maxLength } = schema;

const loading = context.injectLoading();
const lengthIsExceeded = computed(() => maxLength && data.length > maxLength);
const text = computed(() => lengthIsExceeded.value ? truncate(data, maxLength) : data);
const tooltip = computed(() => ({
  value: `${data}`,
  disabled: !lengthIsExceeded.value,
  pt: {
    root: {
      style: maxLength ? `min-width: ${maxLength / 4}rem;` : ""
    }
  }
}));
</script>
