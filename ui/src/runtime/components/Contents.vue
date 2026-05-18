<template>
  <div
    class="
      b-Contents w-full flex gap-8
      max-md:flex-col
    "
    :class="{ 'max-w-screen-xl 3xl:max-w-screen-2xl': !fullScreen }"
  >
    <div
      class="
        w-full
        grid grid-cols-1 items-start gap-4
        lg:grid-cols-2
      "
    >
      <slot v-if="$slots.default" />
      <Bake
        v-for="content in mainContents"
        :key="`content-${content.key}`"
        :name="`${mergedNamePrefix}/${content.key}`"
        :descriptor="content.component"
        :class="{ 'lg:col-span-2': !content.narrow }"
      />
    </div>
    <div
      v-if="sideContents.length"
      class="
        w-[30rem] gap-2 flex flex-col
        max-md:w-full
        max-lg:w-80
      "
    >
      <template
        v-for="(content, i) in sideContents"
        :key="`content-${content.key}`"
      >
        <Divider v-if="i > 0" />
        <Bake
          :name="`${mergedNamePrefix}/${content.key}`"
          :descriptor="content.component"
        />
      </template>
    </div>
  </div>
</template>
<script setup>
import { computed } from "vue";
import { Divider } from "primevue";
import { Bake } from "#components";

const { contents, namePrefix } = defineProps({
  contents: { type: Array, default: () => [] },
  namePrefix: { type: String, default: null },
  fullScreen: { type: Boolean, default: false }
});

const mergedNamePrefix = namePrefix ? `${namePrefix}/contents` : "contents";
const mainContents = computed(() => contents.filter(c => !c.side));
const sideContents = computed(() => contents.filter(c => c.side));
</script>