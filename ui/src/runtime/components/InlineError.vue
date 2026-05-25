<template>
  <div
    class="
      rounded-md pl-1
      bg-red-500/10 text-red-500
    "
  >
    <span
      v-tooltip.bottom="{
        value: `${summary} - ${detail}`,
        autoHide: false,
        pt: {
          text: { class: 'bg-red-500' },
          arrow: { class: 'border-b-red-500' }
        }
      }"
      class="text-sm"
    >{{ summary }}</span>
  </div>
</template>
<script setup>
const { error } = defineProps({
  error: { type: Object, required: true }
});

const { formatted: { summary, detail } } = error;
</script>