<template>
  <Menubar :model="variantLinks" />
  <div
    v-for="variant in variants"
    :key="variant.name"
    :data-testid="variant.name"
    class="my-4"
  >
    <h2 :id="variant.name" class="text-xl font-bold mb-2">{{variant.name}}</h2>
    <Baked.Component :descriptor="variant.descriptor" />
  </div>
</template>
<script setup>
const { variants } = defineProps({
  variants: {
    type: Array,
    required: true
  }
});

const variantLinks = computed(() => variants.map(v => ({ label: v.name, url: `#${v.name}`})));
</script>
