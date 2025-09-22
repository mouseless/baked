<template>
  <Toast position="top-center" />
  <Bake
    :key="descriptor.type"
    name="root"
    :descriptor="descriptor"
  >
    <slot />
  </Bake>
</template>
<script setup>
import { ref, watch } from "vue";
import { useRoute } from "#app";
import { Toast } from "primevue";
import { useLayouts, usePages } from "#imports";
import { Bake } from "#components";

const route = useRoute();
const layouts = useLayouts();
const pages = usePages();

const descriptor = ref(await findLayout(route.params.baked?.[0]));
watch(
  () => route.params.baked?.[0],
  async(newPageName, oldPageName) => {
    if(newPageName === oldPageName) { return; }

    descriptor.value = await findLayout(newPageName);
  }
);

async function findLayout(pageName) {
  const pageDescriptor = await pages.fetch(pageName || "index", { throwNotFound: false });

  return await layouts.fetch(pageDescriptor?.schema?.layout || "default");
}
</script>
<style>
.bg-body {
  background-color: white;
}

html {
  @apply max-xs:text-[smaller];
}

.p-scrolltop {
  padding-top: calc(var(--p-button-icon-only-width) / 2);
  padding-bottom: calc(var(--p-button-icon-only-width) / 2);

  @apply max-md:bottom-24;
}

.p-tooltip {
  @apply !z-[1002];
}

.p-toast {
  @apply !z-[2000];
}

@media (prefers-color-scheme: dark) {
  .bg-body {
    background-color: #121212;
  }
}
</style>
