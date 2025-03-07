<template>
  <span>{{ data }}</span>
</template>
<script setup>
defineProps({
  schema: { type: null, default: null },
  data: { type: null, required: true }
});
</script>
