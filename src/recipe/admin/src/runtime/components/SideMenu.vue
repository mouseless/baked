<template>
  <nav
    class="
      p-2 md:p-4 shadow-inner bg-slate-100 dark:bg-zinc-900
      flex flex-row justify-between md:flex-col md:justify-start items-center gap-2
    "
  >
    <div class="flex item-center justifty-center w-10">
      <RouterLink to="/">
        <img
          :src="`/${logo}`"
          class="my-4 w-8"
        >
      </RouterLink>
    </div>
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

const { logo, menu, footer } = schema;

const loading = context.loading();
</script>
