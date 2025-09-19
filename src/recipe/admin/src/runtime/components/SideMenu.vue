<template>
  <div
    class="
      b-component--SideMenu--placeholder
      min-w-[5.25rem]
      max-md:min-h-16 max-md:mt-1 max-md:w-full
      2xl:min-w-64
    "
  />
  <nav
    v-bind="$attrs"
    class="
      fixed z-20
      p-4 bg-slate-100 dark:bg-zinc-900
      flex flex-col justify-start gap-2
      md:top-0 md:left-0 md:h-screen
      max-md:bottom-0 max-md:w-full max-md:p-2
      max-md:flex-row max-md:justify-between max-md:items-center
      max-md:border-t max-md:border-slate-300 max-md:dark:border-zinc-800
      max-md:drop-shadow-[0_-2px_2px_rgba(0,0,0,0.1)]
      2xl:min-w-64
    "
  >
    <RouterLink
      to="/"
      class="
        flex mt-4 mb-8 w-full min-w-[3.25rem]
        max-2xs:hidden
        max-md:my-0 max-md:w-10
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
        <Skeleton class="py-6 min-w-[3.25rem] max-md:snap-start" />
        <Skeleton class="py-6 min-w-[3.25rem] max-md:snap-start" />
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
          class="w-[3.25rem] h-[3.25rem]"
          variant="text"
          rounded
          @click="togglePopover"
        />
        <PersistentPopover
          ref="popover"
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
import { Bake, Logo, PersistentPopover, SideMenuItem } from "#components";
import { useBreakpoints, useContext } from "#imports";

const { isMd } = useBreakpoints();
const context = useContext();

const { schema, data } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, required: true }
});

const { logo, largeLogo, menu, footer } = schema;

const loading = context.loading();
const popover = ref();

function togglePopover(event) {
  popover.value.toggle(event);
}
</script>
