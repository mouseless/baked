<template>
  <div class="h-screen">
    <div class="flex flex-col gap-8 justify-center items-center h-3/4">
      <slot />
    </div>
  </div>
</template>
<script setup>
defineProps({
  schema: { type: null, default: null },
  data: { type: null, default: null }
});
</script>
