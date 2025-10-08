<template>
  <div class="mx-auto">
    <img
      :src="`/${src}`"
      class="h-8 dark:hidden"
    >
    <img
      :src="`/dark--${src}`"
      class="h-8 hidden dark:block"
    >
  </div>
</template>
<script setup>
defineProps({
  src: { type: null, required: true }
});
</script>
