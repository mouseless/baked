<template>
  <div
    id="page-title"
    class="sticky -top-1 z-10 space-y-4 bg-body"
  >
    <div class="h-16 flex gap-2">
      <div class="w-full flex flex-col gap-2 justify-end">
        <Skeleton
          v-if="loading"
          width="10rem"
          height="1.5rem"
        />
        <h1
          v-else
          class="text-xl font-bold"
        >
          {{ title }}
        </h1>
        <Skeleton
          v-if="loading"
          width="20rem"
          height="1.25rem"
        />
        <div
          v-else
          data-testid="description"
          class="text-sm text-gray-600 dark:text-gray-400"
        >
          {{ description || "&nbsp;" }}
        </div>
      </div>
      <div class="min-w-min pt-6 flex gap-2 row-span-2 items-end text-nowrap">
        <Bake
          v-for="action in actions"
          :key="action.schema.name"
          :name="`actions/${action.schema.name}`"
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
import { useRuntimeConfig } from "#app";
import { Skeleton } from "primevue";
import { useHead } from "#imports";
import { Bake } from "#components";

const { schema } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, default: null },
  loading: { type: Boolean, default: false }
});

const { title, description, actions } = schema;
const { public: { components } } = useRuntimeConfig();
useHead({
  title: components?.Page?.title
    ? `${components.Page.title} - ${title}`
    : title
});

function toggleClasses(element, toggle, classes) {
  for(const cls of classes) {
    element.classList.toggle(cls, toggle);
  }
}

onMounted(() => {
  const el = document.querySelector("#page-title");
  if(!el) { return; }

  const observer = new IntersectionObserver(
    ([e]) => {
      toggleClasses(e.target, e.intersectionRatio < 1,
        [
          "-mx-4", "px-4", "pb-4",
          "border-b", "border-slate-300", "dark:border-zinc-800",
          "drop-shadow"
        ]
      );
    },
    { threshold: [1] }
  );

  try {
    observer.observe(el);
  } catch (e) {
    console.warn(e);
  }
});
</script>
<style scoped>
.sticky {
  top: -1px;
}
</style>
