<template>
  <NuxtLink
    :href="href"
    :target="href.startsWith('http') ? '_blank' : target"
  >
    <slot /> <i
      v-if="href.startsWith('http')"
      class="fa-solid fa-arrow-up-right-from-square"
    />
  </NuxtLink>
</template>
<script setup lang="ts">
withDefaults(defineProps<{
  href: string,
  target?: string
}>(), {
  href: "",
  target: undefined
});
</script>
<style lang="scss" scoped>
a {
  font-family: 'Open Sans';
}
</style>
