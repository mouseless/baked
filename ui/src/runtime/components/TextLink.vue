<template>
  <AwaitLoading :skeleton="{ height: '1.5rem' }">
    <Button
      v-if="rawData"
      v-tooltip.bottom="tooltip"
      :icon
      icon-pos="right"
      :label="text"
      :href="rawData"
      variant="link"
      as="a"
      target="_blank"
      rel="noopener"
      class="m-0 p-0 justify-start text-[length:inherit]"
    />
    <span v-else>-</span>
  </AwaitLoading>
</template>
<script setup>
import { computed } from "vue";
import { Button } from "primevue";
import { useFormat } from "#imports";
import { AwaitLoading } from "#components";

const { truncate } = useFormat();

const { schema, data: rawData } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, required: true }
});

const { icon = "pi pi-external-link", maxLength = 50 } = schema;

const data = computed(() => rawData.includes("://") ? rawData.split("://")[1] : rawData);
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
