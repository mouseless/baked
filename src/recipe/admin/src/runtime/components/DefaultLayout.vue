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
      class="w-full px-4 flex flex-col bg-body mb-4"
    >
      <Bake
        :key="route.path"
        name="header"
        :descriptor="header"
      />
      <slot />
    </article>
  </div>
  <ScrollTop />
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

.p-tooltip {
  @apply !z-[1002];
}

.p-toast {
  @apply !z-[2000];
}
</style>
