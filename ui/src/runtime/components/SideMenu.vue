<template>
  <div
    class="
      b-component--SideMenu--placeholder
      min-w-12 2xl:min-w-50
      max-md:min-h-10 max-md:mt-1 max-md:w-full
    "
  />
  <nav
    v-bind="$attrs"
    class="
      fixed z-10 bg-layout
      flex flex-col justify-start gap-2
      min-w-12 2xl:min-w-50
      md:top-0 md:left-0 md:h-screen
      md:bg-transparent!
      max-md:bottom-0 max-md:w-full max-md:px-2
      max-md:flex-row max-md:justify-between max-md:items-center
      max-md:border-t max-md:border-slate-300 max-md:dark:border-zinc-800
      max-md:drop-shadow-[0_-2px_2px_rgba(0,0,0,0.1)]
      2xl:px-2 2xl:pb-2
    "
  >
    <RouterLink
      to="/"
      class="
        flex self-center
        my-4 w-32
        max-2xl:max-w-3
        max-2xl:mt-1 max-2xl:mb-7 max-2xl:mx-0
        max-2xs:hidden
        max-md:my-0 max-md:mx-2
        logo
      "
    >
      <Logo
        :src="logo"
        class="2xl:hidden"
      />
      <Logo
        :src="largeLogo"
        class="hidden 2xl:block"
      />
    </RouterLink>
    <div
      class="
        flex flex-col gap-2 h-full
        max-md:h-fit max-md:flex-row
        max-md:overflow-x-auto max-md:snap-x
      "
    >
      <template v-if="loading">
        <Skeleton class="py-[1.375rem] min-w-12 rounded-full max-md:snap-start" />
        <Skeleton class="py-[1.375rem] min-w-12 rounded-full max-md:snap-start" />
      </template>
      <template v-else-if="data">
        <SideMenuItem
          v-for="item in menu"
          :key="item.title"
          :item="item"
          :path="data.path"
          class="max-md:snap-start"
        />
      </template>
    </div>
    <div v-if="$slots.footer || footer">
      <div v-if="isMd">
        <Bake
          v-if="footer"
          name="footer"
          :descriptor="footer"
        />
        <slot
          v-else
          name="footer"
        />
      </div>
      <template v-else>
        <Button
          severity="secondary"
          icon="pi pi-cog"
          class="w-10 h-10"
          variant="text"
          rounded
          @click="togglePopover"
        />
        <PersistentPopover
          ref="popover"
          fixed
          class="w-1/2 min-w-fit"
        >
          <Bake
            v-if="footer"
            name="footer"
            :descriptor="footer"
          />
          <slot
            v-else
            name="footer"
          />
        </PersistentPopover>
      </template>
    </div>
  </nav>
</template>
<script setup>
import { ref } from "vue";
import { RouterLink } from "vue-router";
import { Button, Skeleton } from "primevue";
import { useBreakpoints, useContext } from "#imports";
import { Bake, Logo, PersistentPopover, SideMenuItem } from "#components";

const { isMd } = useBreakpoints();
const context = useContext();

const { schema, data } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, required: true }
});

const {
  logo = "logo.svg",
  largeLogo = "logo-full.svg",
  menu,
  footer
} = schema;

const loading = context.injectLoading();
const popover = ref();

function togglePopover(event) {
  popover.value.toggle(event);
}
</script>