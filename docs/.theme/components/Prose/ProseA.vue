<template>
  <NuxtLink
    :href="manipulatedHref"
    :target="manipulatedHref.startsWith('http') ? '_blank' : target"
    :class="{ 'external': external }"
  >
    <slot />
    <i
      v-if="external"
      class="fa-solid fa-arrow-up-right-from-square"
    />
  </NuxtLink>
</template>
<script setup lang="ts">
const props = withDefaults(defineProps<{
  href?: string,
  target?: string
}>(), {
  href: "",
  target: undefined
});

const manipulatedHref = props.href
  .replace("/index.md#", "#")
  .replace("index.md#", "#")
  .replace(".md#", "#");

const external = manipulatedHref.startsWith("http");
</script>
<style lang="scss" scoped>
a {
  font-family: $font-default;
}

i {
  margin-left: $space-xs;
}
</style>
