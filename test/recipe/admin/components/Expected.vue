<template>
  <span :data-testid="schema">{{ data }}</span>
</template>
<script setup>
defineProps({
  schema: { type: null, required: true },
  data: { type: null, required: true }
});
</script>
