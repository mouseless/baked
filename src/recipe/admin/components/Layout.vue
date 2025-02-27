<template>
  <div class="flex h-screen">
    <SideMenu />
    <article class="w-full px-4 flex flex-col bg-body">
      <Header :key="route.path" class="w-full" />
      <slot />
      <ScrollTop target="parent" :pt="{ root: { class: 'min-h-10 min-w-10' } }" />
    </article>
  </div>
</template>
<script setup>
import { ScrollTop } from "primevue";
import SideMenu from "./SideMenu.vue";
import Header from "./Header.vue";

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
