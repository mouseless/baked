<template>
  <pre v-if="language !== 'mermaid'" v-bind="props"><slot/></pre>
  <Mermaid v-else>{{ code }}</Mermaid>
</template>
<script setup>
const props = defineProps({
  class: { type: String, default: null },
  code: { type: String, default: "" },
  filename: { type: String, default: null },
  language: { type: String, default: null },
  meta: { type: String, default: null }
});
</script>