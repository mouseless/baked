<template>
  <Loading :skeleton="{ height: '1.5rem' }">
    <span
      v-if="data"
      v-tooltip.bottom="tooltip"
      v-bind="$attrs"
    >{{ text }}</span>
    <span v-else>-</span>
  </Loading>
</template>
<script setup>
import { computed } from "vue";
import { useFormat } from "#imports";
import { Loading } from "#components";

const { truncate } = useFormat();

const { schema, data } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, required: true }
});

const { maxLength } = schema;

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
