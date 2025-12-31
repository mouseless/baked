<template>
  <i
    class="pi"
    :class="iconClass"
  />
</template>
<script setup>
const { schema } = defineProps({
  schema: { type: null, required: true }
});

const { iconClass } = schema;
</script>
