<template>
  <Bake
    v-if="layoutDescriptor"
    name="root"
    :descriptor="layoutDescriptor"
  >
    <slot />
  </Bake>
  <Toast />
</template>
<script setup>
import { onMounted, ref } from "vue";
import { Toast } from "primevue";
import { useLayouts } from "#imports";
import Bake from "./Bake.vue";

const { name } = defineProps({
  name: { type: String, required: true }
});

const layouts = useLayouts();
const layoutDescriptor = ref();

onMounted(async() => layoutDescriptor.value = await layouts.fetch(name));
</script>
<style lang="scss">
.bg-body {
  background-color: white;
}

@media (prefers-color-scheme: dark) {
  .bg-body {
    background-color: #121212;
  }
}
</style>
