<template>
  <div
    class="
      flex items-start my-sm gap-md
      max-md:flex-col max-md:gap-sm
    "
  >
    <div
      v-for="i in Array(count)
        .fill(0)
        .map((_, i) => i)"
      :key="i"
      class="c--column max-md:w-full!"
      :style="`width: ${itemWidths[i] || itemWidth}`"
    >
      <slot :name="items[i]" />
    </div>
  </div>
</template>
<script setup>
defineProps({
  itemWidth: { type: String, default: "100%"},
  itemWidths: { type: Array, default: () => [] }
});
const slots = useSlots();

const items = Object.keys(slots);
const count = computed(() => items.length);
</script>