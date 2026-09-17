<template>
  <AwaitLoading>
    <span v-if="data === true">
      <i
        class="
          text-xs pi pi-check
          text-green-600 dark:text-green-400
        "
      />
    </span>
    <span v-else-if="data === false">
      <i
        class="
          text-xs pi pi-times
          text-red-600 dark:text-red-400
        "
      />
    </span>
    <span v-else>-</span>
  </AwaitLoading>
</template>
<script setup>
import { AwaitLoading } from "#components";

defineProps({
  data: { type: null, required: true }
});
</script>
