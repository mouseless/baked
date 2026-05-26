<template>
  <Toast
    position="top-center"
    :pt="{
      root: ({ state }) => ({
        class: ['z-10', state.messages[0]?.wide ? 'w-4/5' : '']
      }) 
    }"
  />
  <Bake
    :key="descriptor.type"
    name="root"
    :descriptor="descriptor"
  >
    <slot />
  </Bake>
</template>
<script setup>
import { reactive, ref, watch } from "vue";
import { useRoute } from "#app";
import { Toast } from "primevue";
import { useContext, useEvents, useLayouts, usePages } from "#imports";
import { Bake } from "#components";

const context = useContext();
const events = useEvents();
const layouts = useLayouts();
const pages = usePages();
const route = useRoute();

context.provideEvents(events);
context.providePageContext(reactive({}));

const descriptor = ref(await findLayout(route.matched[0].name));
watch(
  () => route.matched[0].name,
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
