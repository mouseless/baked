<template>
  <Bake
    v-if="layoutDescriptor"
    :descriptor="layoutDescriptor"
  >
    <slot />
  </Bake>
</template>
<script setup>
import Bake from "./Bake.vue";
import useLayouts from "../composables/useLayouts.mjs";

const { name } = defineProps({
  name: { type: String, required: true }
});

const layouts = useLayouts();
const layoutDescriptor = ref();

onMounted(async() => layoutDescriptor.value = await layouts.fetch(name));
</script>
