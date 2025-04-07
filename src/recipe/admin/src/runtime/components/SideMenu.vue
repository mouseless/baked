<template>
  <nav
    class="
      p-4 shadow-inner bg-slate-100 dark:bg-zinc-900
      flex flex-col items-center gap-2
    "
  >
    <RouterLink to="/">
      <img
        :src="`/${logo}`"
        class="my-4 w-8"
      >
    </RouterLink>
    <div
      v-if="loading"
      class="space-y-2 flex flex-col gap-2"
    >
      <Skeleton size="3.1rem" />
      <Skeleton size="3.1rem" />
    </div>
    <div
      v-else-if="data"
      class="space-y-2 flex flex-col gap-2"
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
      class="mt-auto flex flex-col items-center gap-2"
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
