<template>
  <AwaitLoading :skeleton="{ height: '1.5rem' }">
    <span
      v-if="data"
      v-tooltip.bottom="tooltip"
      class="max-sm:select-none"
    >{{ display }}</span>
    <span v-else>-</span>
  </AwaitLoading>
</template>
<script setup>
import { computed } from "vue";
import { useFormat } from "#imports";
import { AwaitLoading } from "#components";

const { asNumber } = useFormat();

const { data } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, required: true }
});

const display = computed(() => asNumber(data));
const tooltip = computed(() => display.value.shortened ? `${asNumber(data, { shorten: false })}` : null);
</script>
