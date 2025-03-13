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
    <Skeleton
      v-if="loading"
      size="3.1rem"
    />
    <Skeleton
      v-if="loading"
      size="3.1rem"
    />
    <SideMenuItem
      v-for="item in menu"
      v-else
      :key="item.title"
      :item="item"
      :path="data.path"
    />
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
import Bake from "./Bake.vue";
import SideMenuItem from "./SideMenuItem.vue";

const { schema, data } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, required: true },
  loading: { type: Boolean, default: false }
});

const { logo, menu, footer } = schema;
</script>
