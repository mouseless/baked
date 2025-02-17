<template>
  <Menubar :model="data" />
</template>
<script setup>
const { data } = defineProps({
  data: { type: null, required: true }
});
</script>
