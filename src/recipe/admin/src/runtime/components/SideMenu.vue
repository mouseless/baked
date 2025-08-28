<template>
  <nav
    class="
      p-4 bg-slate-100 dark:bg-zinc-900
      flex flex-col justify-start gap-2
      max-md:p-2 max-md:h-16
      max-md:flex-row max-md:justify-between
      max-md:items-center
      2xl:min-w-64
    "
  >
    <RouterLink
      to="/"
      class="
        flex mt-4 mb-8 w-full
        md:min-w-[3.25rem]
        max-md:w-10 max-md:my-0
      "
    >
      <img
        :src="`/${logo}`"
        class="mx-auto h-8 2xl:hidden"
      >
      <img
        :src="`/${largeLogo}`"
        class="mx-auto h-8 hidden 2xl:block"
      >
    </RouterLink>
    <div
      class="
        flex flex-col gap-2
        max-md:flex-row
      "
    >
      <template v-if="loading">
        <Skeleton class="py-6 min-w-[3.25rem]" />
        <Skeleton class="py-6 min-w-[3.25rem]" />
      </template>
      <template v-else-if="data">
        <SideMenuItem
          v-for="item in menu"
          :key="item.title"
          :item="item"
          :path="data.path"
        />
      </template>
    </div>
    <div
      v-if="$slots.footer || footer"
      class="flex flex-col gap-2 max-md:flex-col"
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
