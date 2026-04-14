<template>
  <AwaitLoading :skeleton="{ height: '1.5rem' }">
    <span
      v-if="data"
      v-tooltip.bottom="tooltip"
    >{{ text }}</span>
    <span v-else>-</span>
  </AwaitLoading>
</template>
<script setup>
import { computed } from "vue";
import { useFormat } from "#imports";
import { AwaitLoading } from "#components";

const { truncate } = useFormat();

const { schema, data: rawData } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, required: true }
});

const { maxLength, prop } = schema;

const data = computed(() => {
  if(!rawData) { return null; }
  if(prop) { return rawData[prop]; }

  return rawData;
});
const lengthIsExceeded = computed(() => maxLength && data.value.length > maxLength);
const text = computed(() => lengthIsExceeded.value ? truncate(data.value, maxLength) : data.value);
const tooltip = computed(() => ({
  value: `${data.value}`,
  disabled: !lengthIsExceeded.value,
  pt: {
    root: {
      style: maxLength ? `min-width: ${maxLength / 4}rem;` : ""
    }
  }
}));
</script>