<template>
  <nav
    class="
      p-2 shadow-inner bg-slate-100 dark:bg-zinc-900
      flex flex-row justify-between gap-2
      max-md:items-center md:p-4 md:flex-col md:justify-start 2xl:w-80
    "
  >
    <RouterLink
      to="/"
      class="flex w-10 md:w-full"
    >
      <img
        :src="`/${logo}`"
        class="my-4 mx-auto h-8 2xl:hidden"
      >
      <img
        :src="`/${largeLogo}`"
        class="my-4 px-2 h-8 hidden 2xl:block"
      >
    </RouterLink>
    <div
      v-if="loading"
      class="md:space-y-2 flex flex-row md:flex-col gap-2"
    >
      <Skeleton size="3.1rem" />
      <Skeleton size="3.1rem" />
    </div>
    <div
      v-else-if="data"
      class="md:space-y-2 flex flex-row md:flex-col gap-2"
    >
      <SideMenuItem
        v-for="item in menu"
        :key="item.title"
        :item="item"
        :path="data.path"
      />
    </div>
    <div
      v-if="$slots.footer || footer"
      class="md:mt-auto flex flex-row md:flex-col items-center gap-2"
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
    </div>
  </nav>
</template>
<script setup>
import { RouterLink } from "vue-router";
import { Skeleton } from "primevue";
import { Bake, SideMenuItem } from "#components";
import { useContext } from "#imports";

const context = useContext();

const { schema, data } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, required: true }
});

const { logo, largeLogo, menu, footer } = schema;

const loading = context.loading();
</script>
