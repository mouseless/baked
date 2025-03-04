<template>
  <Bake
    v-if="layoutDescriptor"
    :descriptor="layoutDescriptor"
  >
    <slot />
  </Bake>
</template>
<script setup>
import { onMounted, ref } from "vue";
import { useLayouts } from "#app";
import Bake from "./Bake.vue";

const { name } = defineProps({
  name: { type: String, required: true }
});

const layouts = useLayouts();
const layoutDescriptor = ref();

onMounted(async() => layoutDescriptor.value = await layouts.fetch(name));
</script>
