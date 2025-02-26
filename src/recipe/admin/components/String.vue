<template>
  <span>{{ data }}</span>
</template>
<script setup>
defineProps({
  schema: { type: null, required: true },
  data: { type: null, required: true }
});
</script>
