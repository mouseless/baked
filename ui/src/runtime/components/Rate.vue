<template>
  <Renderer
    :skeleton="{ height: '1.5rem' }"
    :when="data"
  >
    <template #content>
      <span>{{ display }}</span>
    </template>
  </Renderer>
</template>
<script setup>
import { computed } from "vue";
import { useFormat } from "#imports";
import { Renderer } from "#components";

const { asPercentage } = useFormat();

const { data } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, required: true }
});

const display = computed(() => asPercentage(data));
</script>
