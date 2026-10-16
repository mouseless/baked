<template>
  <picture>
    <source
      v-if="darkSrc"
      :srcset="darkSrc"
      media="(prefers-color-scheme: dark)"
    >
    <img
      :src="refinedSrc"
      :alt="alt"
      :width="width"
      :height="height"
      :class="[ alt ]"
      class="max-w-full my-4 rounded-xs shadow-md"
      @error="darkMissing = true"
    >
  </picture>
</template>
<script setup>
import { ref } from "vue";
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
const darkMissing = ref(false);

const darkSrc = computed(() => {
  if(darkMissing.value || props.src.startsWith("//")) { return null; }

  return refinedSrc.value.replace(/(\.[^./]+)$/, "-dark$1");
});

const refinedSrc = computed(() => {
  if(props.src.startsWith("//")) {
    return props.src;
  }

  const base = withLeadingSlash(withTrailingSlash(useRuntimeConfig().app.baseURL));
  const path = parsePath(route);
  const result = joinURL(base, path, props.src);

  return result;
});

function parsePath(route) {
  const param = route.params.contentpage;
  const pieces = (Array.isArray(param) ? param : [param]).filter(piece => piece);

  return pieces.length > 1
    ? joinURL(...pieces.slice(0, pieces.length - 1))
    : pieces[0] ?? "/";
}
</script>
