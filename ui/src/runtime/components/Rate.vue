<template>
  <AwaitLoading :skeleton="{ height: '1.5rem' }">
    <span v-if="data">{{ display }}</span>
    <span v-else>-</span>
  </AwaitLoading>
</template>
<script setup>
import { computed } from "vue";
import { useFormat } from "#imports";
import { AwaitLoading } from "#components";

const { asPercentage } = useFormat();

const { data } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, required: true }
});

const display = computed(() => asPercentage(data));
</script>
