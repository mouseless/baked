<template>
  <img
    :src="refinedSrc"
    :alt="alt"
    :width="width"
    :height="height"
    :class="[ alt ]"
    @load="onImgLoad"
  >
</template>
<script setup lang="ts">
import { withBase } from "ufo";
import { useRuntimeConfig, computed } from "#imports";

const props = withDefaults(defineProps<{
  src: string,
  alt: string,
  width: string | number,
  height: string | number
}>(), {
  src: "",
  alt: "",
  width: undefined,
  height: undefined
});

const refinedSrc = computed(() => {
  if(props.src?.startsWith("/") && !props.src.startsWith("//")) {
    return withBase(props.src, useRuntimeConfig().app.baseURL);
  }

  return props.src;
});

</script>
