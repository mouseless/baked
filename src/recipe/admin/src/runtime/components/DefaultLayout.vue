<template>
  <div class="flex h-screen">
    <Bake
      name="sideMenu"
      :descriptor="schema.sideMenu"
    />
    <article class="w-full px-4 flex flex-col bg-body">
      <Bake
        :key="route.path"
        name="header"
        :descriptor="schema.header"
      />
      <slot />
      <ScrollTop target="parent" />
    </article>
  </div>
</template>
<script setup>
import { useRoute } from "#app";
import { ScrollTop } from "primevue";
import { Bake } from "#components";

const { schema } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, default: null }
});

// do NOT remove this without testing. using $route in template doesn't trigger
// header refresh properly, using setup variable solved the issue.
const route = useRoute();
</script>
<style scoped>
/* overflow-x-hidden fixes chart auto width problem under this parent */
/* see: https://stackoverflow.com/questions/52502837/chart-js-in-flex-element-overflows-instead-of-shrinking */
article {
  overflow-x: hidden;
}
</style>
<style>
.p-scrolltop {
  padding-top: calc(var(--p-button-icon-only-width) / 2);
  padding-bottom: calc(var(--p-button-icon-only-width) / 2);
}
</style>
