<template>
  <div
    id="page-title"
    class="sticky -top-1 z-10 space-y-4 bg-body"
  >
    <div class="h-16 flex gap-2">
      <div class="w-full flex flex-row items-center gap-2 justify-start">
        <h1 class="text-xl font-bold">
          {{ l(title) }}
        </h1>
        <div class="relative group">
          <div
            data-testid="description"
            class="
              text-sm text-gray-600 dark:text-gray-400
              text-nowrap overflow-hidden
              hidden 2xl:block
            "
          >
            <String
              :schema="{ maxLength: 125 }"
              :data="l(description) || '&nbsp;'"
            />
          </div>
          <button
            v-if="description"
            class="2xl:hidden inline-flex items-center text-gray-500 hover:text-gray-700 dark:text-gray-400 dark:hover:text-gray-300"
          >
            <span class="pi pi-info-circle text-lg" />
            <div class="absolute left-1/2 transform -translate-x-1/2 top-full mt-2 hidden group-hover:block z-10">
              <div class="bg-white dark:bg-gray-800 rounded-lg shadow-lg p-4 max-w-sm b-tooltip">
                <p class="text-sm text-gray-600 dark:text-gray-400">
                  {{ l(description) }}
                </p>
              </div>
            </div>
          </button>
        </div>
      </div>
      <div
        v-focustrap
        class="min-w-min pt-6 flex gap-2 row-span-2 items-end text-nowrap"
      >
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
import { Bake, String } from "#components";
import { useHead, useLocalization } from "#imports";

const { localize: l } = useLocalization();
const { public: { components } } = useRuntimeConfig();

const { schema } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, default: null }
});

const { title, description, actions } = schema;

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
