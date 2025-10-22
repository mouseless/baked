<template>
  <div
    v-bind="$attrs"
    class="
      flex flex-row
      max-md:flex-col-reverse
    "
  >
    <Bake
      name="sideMenu"
      :descriptor="sideMenu"
    />
    <article
      class="
        w-full px-4 flex flex-col bg-body mb-[5.5rem]
        max-md:mb-24
      "
    >
      <Bake
        :key="route.path"
        name="header"
        :descriptor="header"
      />
      <slot />
    </article>
  </div>
  <ScrollTop
    v-if="scrollTopOptions"
    v-bind="scrollTopOptions"
  />
</template>
<script setup>
import { useRoute } from "#app";
import { ScrollTop } from "primevue";
import { Bake } from "#components";

// do NOT remove this without testing. using $route in template doesn't trigger
// header refresh properly, using setup variable solved the issue.
const route = useRoute();

const { schema } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, default: null }
});

const { header, sideMenu, layoutOptions } = schema;
const scrollTopOptions = layoutOptions?.scrollTopOptions || { threshold: 500, icon: "pi pi-arrow-up" };
</script>
<style>
.p-scrolltop {
  padding-top: calc(var(--p-button-icon-only-width) / 2);
  padding-bottom: calc(var(--p-button-icon-only-width) / 2);

  @apply right-6 bottom-6 max-md:bottom-24;
}
</style>
