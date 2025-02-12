<template>
  {{ data }}
</template>
<script setup>
const { data } = defineProps({
  schema: {
    type: Object,
    required: true
  },
  data: {
    type: Object,
    required: true
  }
});
</script>
