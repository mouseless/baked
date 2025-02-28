<template>
  <nav
    class="
      p-4 shadow-inner bg-slate-100 dark:bg-zinc-900
      flex flex-col items-center gap-2
    "
  >
    <img
      :src="`/${logo}`"
      class="my-4 w-8"
    >
    <SideMenuItem
      v-for="item in menu"
      :key="item.title"
      :item="item"
      :path="data.path"
    />
    <div
      v-if="$slots.bottom"
      class="mt-auto flex flex-col items-center gap-2"
    >
      <slot name="bottom" />
    </div>
  </nav>
</template>
<script setup>
import SideMenuItem from "./SideMenuItem.vue";

const { schema, data } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, required: true }
});

const { logo, menu } = schema;
</script>
