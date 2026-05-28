<template>
  <Popover
    ref="popoverRef"
    class="bg-transparent border-none before:content-none after:content-none"
    pt:content="p-0"
  >
    <slot />
  </Popover>
</template>
<script setup>
import { ref } from "vue";
import { Popover } from "primevue";

defineExpose({ show, hide });

const popoverRef = ref();

function show(target) {
  popoverRef.value.show({ currentTarget: target.value.$el });
}

function hide() {
  popoverRef.value.hide();
}
</script>