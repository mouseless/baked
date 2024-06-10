<template>
  <div
    class="columns s s--mv_sm"
  >
    <div
      v-for="i in Array(count)
        .fill(0)
        .map((_, i) => i)"
      :key="i"
      class="columns__item"
      :style="`width: ${itemWidths[i] || itemWidth}`"
    >
      <slot :name="items[i]" />
    </div>
  </div>
</template>
<script setup>
defineProps({
  itemWidth: {
    type: String,
    default: "100%"
  },
  itemWidths: {
    type: Array,
    default: () => []
  }
});
const slots = useSlots();

const items = Object.keys(slots);
const count = computed(() => items.length);
</script>
<style lang="scss">
.columns {
  display: flex;
  gap: var(--space-md);
  align-items: flex-start;

  &__item {
    max-width: 100%;
  }
}

@media (max-width: $width-page-m) {
  .columns {
    flex-direction: column;
    gap: var(--space-sm);

    &__item {
      width: 100% !important;
    }

    &__item:has(img.prose:only-child) {
      display: none;
    }
  }
}
</style>
