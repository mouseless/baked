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
    <SideMenuItem
      v-for="item in menu"
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
import Bake from "./Bake.vue";
import SideMenuItem from "./SideMenuItem.vue";

const { schema, data } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, required: true }
});

const { logo, menu, footer } = schema;
</script>
