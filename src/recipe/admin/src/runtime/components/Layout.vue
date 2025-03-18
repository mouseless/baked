<template>
  <Bake
    v-if="layoutDescriptor"
    name="root"
    :descriptor="layoutDescriptor"
  >
    <slot />
  </Bake>
  <Toast />

  <!--
    Skeleton style not loading in production, probably due to `v-if="loading"`
    delays it. Below ensures styles are loaded properly.

    This is a temporary solution, remove this comment and below hidden
    component if you solve it permanently.
  -->
  <Skeleton class="hidden" />
</template>
<script setup>
import { onMounted, ref } from "vue";
import { Skeleton, Toast } from "primevue";
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
