<template>
  <i
    class="pi"
    :class="iconClass"
  />
</template>
<script setup>
const { schema } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, default: null }
});

const { iconClass } = schema;
</script>
