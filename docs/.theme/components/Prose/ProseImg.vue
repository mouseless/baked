<template>
  <img
    :src="refinedSrc"
    :alt="alt"
    :width="width"
    :height="height"
    :class="[ alt ]"
  >
</template>
<script setup>
import { withTrailingSlash, withLeadingSlash, joinURL } from "ufo";
import { useRuntimeConfig, computed, useRoute } from "#imports";

const props = defineProps({
  src: {
    type: String,
    required: true
  },
  alt: {
    type: String,
    default: ""
  },
  width: {
    type: [String, Number],
    default: undefined
  },
  height: {
    type: [String, Number],
    default: undefined
  }
});

const route = useRoute();

const refinedSrc = computed(() => {
  if(props.src.startsWith("//")) {
    return props.src;
  }

  const base = withLeadingSlash(withTrailingSlash(useRuntimeConfig().app.baseURL));
  const path = parsePath(route);
  const result = joinURL(base, path, props.src);

  return result;
});

function parsePath(path) {
  const pieces = path.params.contentpage;

  if(pieces[pieces.length - 1] === "")
  {
    pieces.pop();
  }

  return pieces.length > 1
    ? joinURL(...pieces.slice(0, pieces.length - 1))
    : pieces[0];
}
</script>
<style lang="scss" scoped>
img {
  max-width: 100%;
}
</style>
