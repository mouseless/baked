<template>
  <Message :severity="severity">
    <template
      v-if="icon"
      #icon
    >
      <i :class="`pi ${icon}`" />
    </template>
    <span class="ml-3 whitespace-pre">{{ data }}</span>
  </Message>
</template>
<script setup>
import { Message } from "primevue";

const { schema } = defineProps({
  schema: { type: null, default: null },
  data: { type: null, default: null }
});

const { icon, severity } = schema;
</script>
