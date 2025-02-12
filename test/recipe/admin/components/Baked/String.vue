<template>
  {{ data }}
</template>
<script setup>
const { data } = defineProps({
  schema: { required: true },
  data: { required: true }
});
</script>
