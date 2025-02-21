<template>
  <Menubar v-if="schema" :model="schema.items" />
</template>
<script setup>
const { schema} = defineProps({
  schema: { type: null, required: true },
  data: { type: null, required: true }
});
</script>
