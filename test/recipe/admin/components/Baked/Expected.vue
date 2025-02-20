<template>
  <span :data-testid="schema">{{ data }}</span>
</template>
<script setup>
const { data } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, required: true }
});
</script>
