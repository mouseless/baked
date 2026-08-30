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
  data: { type: null, default: null }
});

const { maxLength, prop } = schema;

const data = computed(() => {
  if(!rawData) { return null; }
  if(prop) { return rawData[prop]; }

  return rawData;
});
const text = computed(() => truncate(data.value, maxLength));
const tooltip = computed(() => ({
  value: `${data.value}`,
  disabled: text.value === data.value,
  pt: {
    root: {
      style: maxLength ? `min-width: ${maxLength / 2}rem;` : ""
    }
  }
}));
</script>