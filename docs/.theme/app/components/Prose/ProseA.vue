<template>
  <NuxtLink
    :href="manipulatedHref"
    :target="manipulatedHref.startsWith('http') ? '_blank' : target"
    class="font-default underline"
    :class="{ 'external': external }"
  >
    <slot />
    <i
      v-if="external"
      class="fa-solid fa-arrow-up-right-from-square ml-xs"
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

const route = useRoute();

const external = props.href.startsWith("http");
const manipulatedHref = external
  ? props.href
  : resolve(props.href
    .replace(/\/?index\.md(#|$)/, "$1")
    .replace(/\.md(#|$)/, "$1"));

// NOTE section index pages (e.g. `/recipes`) come from `README.md` inside the
// section folder, so their relative links resolve against the folder
function resolve(href) {
  if(href === "" || href.startsWith("#") || href.startsWith("/")) { return href; }

  const pieces = route.path.split("/").filter(piece => piece);
  const base = pieces.length === 1 ? `/${pieces[0]}/` : route.path;
  const url = new URL(href, `http://localhost${base}`);

  return `${url.pathname}${url.hash}`;
}
</script>
