<template>
  <div
    id="page-title"
    class="sticky -top-1 z-10 space-y-4 bg-body"
  >
    <div class="flex justify-between items-center gap-2">
      <div
        class="
          w-full flex justify-between
          max-2xs:flex-col gap-2
        "
        :class="{
          'max-2xs:flex-col': actions.length < earlyWrapActionsAt,
          'max-xs:flex-col': actions.length >= earlyWrapActionsAt
        }"
      >
        <div class="flex gap-4 items-center">
          <Bake
            v-if="icon"
            name="icon"
            :descriptor="icon"
            class="min-w-16 max-md:hidden"
          />
          <div
            class="
              w-full mt-1
              flex flex-row
              items-center justify-start
              md:flex-col md:items-start md:mt-2
              max-md:gap-2
            "
          >
            <AwaitLoading
              :skeleton="{
                width: '15rem',
                height: '1.75rem',
                class: 'max-xs:max-w-[5rem]'
              }"
            >
              <div class="grid">
                <h1 class="font-bold text-xl truncate">
                  {{ title }}
                </h1>
              </div>
            </AwaitLoading>
            <PageTitleDescription
              :info-fields
              :description
            />
          </div>
        </div>
        <div
          v-if="actions.length || $slots.actions"
          class="
            actions
            min-w-min flex gap-2 row-span-2 items-end text-nowrap
            max-xs:text-xs max-md:text-sm
            md:max-md:items-center md:pt-6
          "
        >
          <Bake
            v-for="action in actions"
            :key="action.schema.name"
            :name="`actions/${action.schema.name}`"
            :descriptor="action"
          />
          <slot name="actions" />
        </div>
      </div>
      <div
        v-if="$slots.inputs"
        v-focustrap
        class="
          min-w-min flex gap-2 row-span-2 items-end text-nowrap
          max-md:text-sm md:max-md:items-center md:pt-6
        "
      >
        <template v-if="isLg">
          <slot name="inputs" />
        </template>
        <template v-else>
          <Button
            variant="text"
            icon="pi pi-filter"
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
                gap-4 text-sm px-2 py-2
              "
            >
              <slot name="inputs" />
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

const { isLg } = useBreakpoints();
const { localize: l } = useLocalization();
const { public: { components } } = useRuntimeConfig();

const { schema, data } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, required: true }
});

const { actions, description, earlyWrapActionsAt = 4, icon, infoFields, localizeTitle, titleProp } = schema;

const titleData = titleProp ? data?.[titleProp] : data;
const title = titleData ? localizeTitle ? l(titleData) : titleData : "...";
const popoverInputs = ref();

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
          "sticked", "-mx-4", "-mb-4", "p-4",
          "border-b", "border-slate-300", "dark:border-zinc-800",
          "drop-shadow", "z-[9]"
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
</script>
<style scoped>
.sticky {
  top: -1px;
}
</style>
<style>
.b-component--PageTitle {
  .p-button {
    @apply self-stretch;

    .p-button-icon+.p-button-label {
      @apply max-sm:hidden;
    }
  }
}
</style>