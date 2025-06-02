<template>
  <span :data-testid="schema">{{ l(data) }}</span>
</template>
<script setup>
import { useLocalization } from "#imports";

const { localize: l } = useLocalization();

defineProps({
  schema: { type: null, required: true },
  data: { type: null, required: true }
});
</script>
