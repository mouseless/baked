<template>
  <code class="bg-red-500 p-4 rounded-md">
    {{ json }}
  </code>
</template>
<script setup>
import { computed } from "vue";

const { data } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, required: true }
});

const json = computed(() => JSON.stringify(data));
</script>
