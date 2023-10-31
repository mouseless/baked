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
import { withTrailingSlash, withLeadingSlash, joinURL } from "ufo";
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
  if(props.src.startsWith("//")) {
    return props.src;
  }

  const base = withLeadingSlash(withTrailingSlash(useRuntimeConfig().app.baseURL));
  const path = parsePath(route.path);

  return joinURL(base, path, props.src);
});

function parsePath(path: string) {
  return path.endsWith("/")
    ? path
    : path.replace(/\/[^/]*\/?$/, "");
}
</script>
<style lang="scss" scoped>
img {
  max-width: 100%;
}
</style>
