<template>
  <div class="flex justify-center w-full">
    <div
       class="max-w-screen-xl flex gap-4 align-top"
       :class="{
         'flex-col': !vertical,
         'items-center': !vertical,
         'items-start': vertical
       }"
    >
      <div
        v-for="variant in variants"
        :key="variant.name"
        :data-testid="variant.name"
        :class="{
          'w-full': !vertical,
          'text-center': vertical
        }"
      >
        <h2
          :id="variant.name"
          class="font-bold"
          :class="{
            'text-xl': !vertical,
            'mb-2': !vertical
          }"
        >{{variant.name}}</h2>
        <Bake :descriptor="variant.descriptor" />
      </div>
    </div>
  </div>
</template>
<script setup>
import { Bake } from "baked-recipe-admin";

const { variants } = defineProps({
  variants: { type: Array, required: true },
  vertical: { type: Boolean, default: false }
});
</script>
