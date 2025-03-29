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
      v-else
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
import { defineAsyncComponent } from "vue";
import { RouterLink } from "vue-router";
const Skeleton = defineAsyncComponent(() => import("primevue/skeleton"));
import Bake from "./Bake.vue";
import SideMenuItem from "./SideMenuItem.vue";

const { schema, data } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, required: true },
  loading: { type: Boolean, default: false }
});

const { logo, menu, footer } = schema;
</script>
