<template>
  <div
    id="page-title"
    class="sticky -top-1 z-10 pb-4 space-y-4 bg-body"
  >
    <div class="h-16 flex gap-2">
      <div class="w-full flex flex-col gap-2 justify-end">
        <h1 class="text-xl font-bold">
          {{ title }}
        </h1>
        <div
          data-testid="description"
          class="text-sm text-gray-600 dark:text-gray-400"
        >
          {{ description || "&nbsp;" }}
        </div>
      </div>
      <div class="min-w-min pt-6 flex gap-2 row-span-2 items-end">
        <Bake
          v-for="action in actions"
          :key="action.key"
          :descriptor="action"
        />
        <slot
          v-if="$slots.actions"
          name="actions"
        />
      </div>
    </div>
    <slot name="extra" />
  </div>
</template>
<script setup>
import { onMounted } from "vue";
import { useHead, useRuntimeConfig } from "#app";
import Bake from "./Bake.vue";

const { schema } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, default: null }
});

const { title, description, actions } = schema;
const { public: { title: appTitle } } = useRuntimeConfig();
useHead({ title: `${appTitle} - ${title}` });

function toggleClasses(element, toggle, classes) {
  for(const cls of classes) {
    element.classList.toggle(cls, toggle);
  }
}

onMounted(() => {
  const el = document.querySelector("#page-title");
  const observer = new IntersectionObserver(
    ([e]) => {
      toggleClasses(e.target, e.intersectionRatio < 1,
        [
          "-mx-4", "px-4",
          "border-b", "border-slate-300", "dark:border-zinc-800",
          "drop-shadow"
        ]
      );
    },
    { threshold: [1] }
  );

  observer.observe(el);
});
</script>
<style scoped>
.sticky {
  top: -1px;
}
</style>
