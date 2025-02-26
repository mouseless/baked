<template>
  <div class="space-y-4">
    <Menubar :model="variantLinks" />
    <div class="w-full flex flex-col items-center">
      <div class="max-w-screen-xl space-y-4">
        <div
          v-for="variant in variants"
          :key="variant.name"
          :data-testid="variant.name"
          class="w-full"
        >
          <h2 :id="variant.name" class="text-xl font-bold mb-2">{{variant.name}}</h2>
          <Bake :descriptor="variant.descriptor" />
        </div>
      </div>
    </div>
  </div>
</template>
<script setup>
import { Bake } from "baked-recipe-admin";

const { variants } = defineProps({
  variants: {
    type: Array,
    required: true
  }
});

const variantLinks = computed(() => variants.map(v => ({ label: v.name, url: `#${v.name}`})));
</script>
