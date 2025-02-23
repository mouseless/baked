<template>
  <span>{{ data }}</span>
</template>
<script setup>
const { data } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, required: true }
});
</script>
