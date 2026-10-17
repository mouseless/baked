<template>
  <div
    v-bind="$attrs"
    class="
      h-screen
      flex flex-row
      max-md:flex-col
    "
  >
    <Bake
      v-if="isMd"
      name="sideMenu"
      :descriptor="sideMenu"
    />
    <article
      class="
        w-full flex flex-col
        max-md:flex-1
      "
    >
      <div class="px-4 my-3 h-4">
        <Bake
          :key="route.path"
          name="header"
          :descriptor="header"
        />
      </div>
      <div class="bg-body flex-1 md:rounded-tl-2xl">
        <div class="mb-[5.5rem] max-md:mb-24 px-4 pt-4">
          <slot />
        </div>
      </div>
    </article>
    <Bake
      v-if="isMaxMd"
      name="sideMenu"
      :descriptor="sideMenu"
    />
  </div>
  <ScrollTop v-bind="scrollTopOptions" />
</template>
<script setup>
import { ScrollTop } from "primevue";
import { useRoute } from "#app";
import { useBreakpoints } from "#imports";
import { Bake } from "#components";

const { isMaxMd, isMd } = useBreakpoints();
// do NOT remove this without testing. using $route in template doesn't trigger
// header refresh properly, using setup variable solved the issue.
const route = useRoute();

const { schema } = defineProps({
  schema: { type: null, required: true }
});

const { header, sideMenu } = schema;
const scrollTopOptions = { threshold: 250, ...schema.scrollTopOptions };
</script>