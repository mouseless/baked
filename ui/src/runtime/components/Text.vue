<template>
  <Renderer
    :skeleton="{ height: '1.5rem' }"
    :when="data"
  >
    <template #content>
      <span
        v-tooltip.bottom="tooltip"
      >{{ text }}</span>
    </template>
  </Renderer>
</template>
<script setup>
import { computed } from "vue";
import { useFormat } from "#imports";
import { Renderer } from "#components";

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
