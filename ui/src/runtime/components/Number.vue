<template>
  <Renderer
    :skeleton="{ height: '1.5rem' }"
    :when="data"
  >
    <template #content>
      <span
        v-tooltip.bottom="tooltip"
        class="max-sm:select-none"
      >{{ display }}</span>
    </template>
  </Renderer>
</template>
<script setup>
import { computed } from "vue";
import { useFormat } from "#imports";
import { Renderer } from "#components";

const { asNumber } = useFormat();

const { data } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, required: true }
});

const display = computed(() => asNumber(data));
const tooltip = computed(() => display.value.shortened ? `${asNumber(data, { shorten: false })}` : null);
</script>
