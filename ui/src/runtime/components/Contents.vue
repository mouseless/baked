<template>
  <div
    class="b-Contents w-full grid grid-cols-1 gap-4 lg:grid-cols-2"
    :class="{ 'max-w-screen-xl 3xl:max-w-screen-2xl': !fullScreen }"
  >
    <Bake
      v-for="content in contents"
      :key="`content-${content.key}`"
      :name="`${mergedNamePrefix}/${content.key}`"
      :descriptor="content.component"
      :class="{ 'lg:col-span-2': !content.narrow }"
    />
  </div>
</template>
<script setup>
import { Bake } from "#components";

const { namePrefix } = defineProps({
  contents: { type: Array, default: () => [] },
  namePrefix: { type: String, default: null },
  fullScreen: { type: Boolean, default: false }
});

const mergedNamePrefix = namePrefix ? `${namePrefix}/contents` : "contents";
</script>
