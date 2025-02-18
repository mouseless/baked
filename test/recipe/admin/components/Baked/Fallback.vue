<template>
  <code>
    <pre>
      {{ json }}
    </pre>
  </code>
</template>
<script setup>
const { data } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, required: true }
});

const json = computed(() => JSON.stringify(data));
</script>
