<template>
  <div
    id="page-title"
    class="sticky -top-1 z-10 space-y-4 bg-body"
  >
    <div class="h-16 flex gap-2">
      <div
        class="
          w-full flex flex-row gap-2
          items-baseline justify-start
          xl:flex-col mt-1 xl:mt-2
        "
      >
        <h1 class="text-xl font-bold">
          {{ l(title) }}
        </h1>
        <div class="relative">
          <div
            data-testid="description"
            class="
              text-sm text-gray-600 dark:text-gray-400
              text-nowrap overflow-hidden
              hidden xl:block
            "
          >
            <String
              :schema="{ maxLength: 125 }"
              :data="l(description) || '&nbsp;'"
            />
          </div>
          <Button
            v-if="description"
            v-tooltip.bottom="{
              value: l(description)
            }"
            class="xl:hidden"
            icon="pi pi-info-circle"
            variant="text"
            size="small"
            rounded
          />
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
import { Button } from "primevue";
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
