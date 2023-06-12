<template>
  <img
    :src="refinedSrc"
    :alt="alt"
    :width="width"
    :height="height"
    :class="[ alt ]"
  >
</template>
<script setup lang="ts">
import { joinURL, withBase } from "ufo";
import { useRuntimeConfig, computed, useRoute } from "#imports";

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

const route = useRoute();

const refinedSrc = computed(() => {
  let src = props.src;

  if(src?.startsWith("./README")) {
    src = joinURL(route.path, src);
  }

  if(src?.startsWith("/") && !src.startsWith("//")) {
    src = withBase(src, useRuntimeConfig().app.baseURL);
  }

  return src;
});

</script>
