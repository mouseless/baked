<template>
  <div
    id="page-title"
    class="
      sticky -top-1 z-10 space-y-4 bg-body
      max-md:space-y-0 max-lg:space-y-2
    "
  >
    <div
      class="
        flex gap-2 items-start
        md:max-xl:items-center
      "
    >
      <div
        class="
          w-full mt-1
          flex flex-row gap-2
          items-baseline justify-start
          xl:flex-col xl:mt-2
        "
      >
        <div class="grid">
          <h1 class="font-bold text-xl truncate">
            {{ l(title) }}
          </h1>
        </div>
        <div class="relative">
          <div
            data-testid="description"
            class="
              text-sm text-gray-600 dark:text-gray-400
              text-nowrap overflow-hidden
              hidden xl:grid
            "
          >
            <span class="truncate">
              {{ l(description) || '&nbsp;' }}
            </span>
          </div>
          <Button
            v-if="description"
            v-tooltip.focus.bottom="{ value: l(description) }"
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
        class="
          min-w-min flex gap-2 row-span-2 items-end text-nowrap
          max-lg:text-sm md:max-xl:items-center xl:pt-6
        "
      >
        <template v-if="isMd">
          <slot
            v-if="$slots.inputs"
            name="inputs"
          />
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
        </template>
        <template v-else>
          <Button
            v-if="$slots.inputs"
            variant="text"
            icon="pi pi-filter"
            class="lg:hidden"
            rounded
            @click="togglePopoverInputs"
          />
          <PersistentPopover
            ref="popoverInputs"
            fixed
          >
            <div
              class="
              flex flex-col flex-start
              justify-between w-full
              gap-4 text-sm px-2 py-2"
            >
              <slot
                v-if="$slots.inputs"
                name="inputs"
              />
            </div>
          </PersistentPopover>
          <Button
            v-if="$slots.actions || actions?.length > 0"
            variant="text"
            icon="pi pi-ellipsis-h"
            class="lg:hidden"
            rounded
            @click="togglePopoverActions"
          />
          <PersistentPopover
            ref="popoverActions"
            fixed
          >
            <div
              class="
              flex flex-col flex-start
              justify-between w-full min-w-52
              gap-4 text-sm px-2 py-2"
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
          </PersistentPopover>
        </template>
      </div>
    </div>
    <slot name="extra" />
  </div>
</template>
<script setup>
import { onMounted, ref } from "vue";
import { Button } from "primevue";
import { useRuntimeConfig } from "#app";
import { useBreakpoints, useHead, useLocalization } from "#imports";
import { Bake, PersistentPopover } from "#components";

const { isMd } = useBreakpoints();
const { localize: l } = useLocalization();
const { public: { components } } = useRuntimeConfig();

const { schema } = defineProps({
  schema: { type: null, required: true }
});

const { title, description, actions } = schema;
const popoverInputs = ref();
const popoverActions = ref();

useHead({
  title: components?.Page?.title
    ? `${components.Page.title} - ${title}`
    : title
});

onMounted(() => {
  const el = document.querySelector("#page-title");
  if(!el) { return; }

  const observer = new IntersectionObserver(
    ([e]) => {
      toggleClasses(e.target, e.intersectionRatio < 1,
        [
          "-mx-4", "px-4", "pb-4",
          "border-b", "border-slate-300", "dark:border-zinc-800",
          "drop-shadow", "z-[9]",
          "md:max-xl:pt-4", "max-md:pt-2"
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

function toggleClasses(element, toggle, classes) {
  for(const cls of classes) {
    element.classList.toggle(cls, toggle);
  }
}

function togglePopoverInputs(event) {
  popoverInputs.value.toggle(event);
}

function togglePopoverActions(event) {
  popoverActions.value.toggle(event);
}
</script>
<style scoped>
.sticky {
  top: -1px;
}
</style>
