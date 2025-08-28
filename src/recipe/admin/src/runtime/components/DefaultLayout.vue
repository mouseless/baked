<template>
  <div
    class="flex h-screen flex-row overflow-hidden"
  >
    <Bake
      name="sideMenu"
      :descriptor="sideMenu"
    />
    <article
      class="
        w-full px-4 flex flex-col bg-body
      "
      :class="{
        'overflow-x-hidden': !overflow,
        'overflow-visible': overflow
      }"
    >
      <Bake
        :key="route.path"
        name="header"
        :descriptor="header"
      />
      <slot />
      <ScrollTop target="parent" />
    </article>
  </div>
</template>
<script setup>
import { ref } from "vue";
import { useRoute } from "#app";
import { ScrollTop } from "primevue";
import { Bake } from "#components";
import { useContext } from "#imports";

const context = useContext();
// do NOT remove this without testing. using $route in template doesn't trigger
// header refresh properly, using setup variable solved the issue.
const route = useRoute();

const { schema } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, default: null }
});

const { header, sideMenu } = schema;

const overflow = ref(false);
context.setArticleOverflow(overflow);
</script>
<style>
html {
  @apply max-xs:text-[smaller];
}

.p-scrolltop {
  padding-top: calc(var(--p-button-icon-only-width) / 2);
  padding-bottom: calc(var(--p-button-icon-only-width) / 2);

  @apply max-md:bottom-24;
}
</style>
