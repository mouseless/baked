<template>
  <nav>
    <NuxtLink
      v-for="menu in menus"
      :key="menu.title"
      :to="menu._path == $route.path ? '' : menu._path"
      class="menu"
      :class="menu._path == root ? 'top' : ''"
    >
      {{ menu.title }}
    </NuxtLink>
  </nav>
</template>
<script lang="ts" setup>
import type { ParsedContent } from "@nuxt/content/dist/runtime/types";
import { useRoute, watch, queryContent } from "#imports";

const route = useRoute();
const root = ref<string>("");
const menus = ref<Pick<ParsedContent, string>[]>([]);

watch(root, async () => {
  const index = await queryContent(root.value)
    .where({ _path: { $eq: root.value } })
    .only(["_path", "title", "position"])
    .findOne();

  const sections = await queryContent(root.value)
    .where({ _path: { $ne: root.value } })
    .only(["_path", "title", "position"])
    .sort(sorter(index.sort))
    .find();

  menus.value = [index, ...sections];
});

const setRoot = (path: string) => root.value = `/${path.split("/")[1]}`;
setRoot(route.path);
watch(route, newRoute => setRoot(newRoute.path));

function sorter(
  { by = "position", order = "asc" } = { }
) {
  return {
    [by]: order === "asc" ? 1 : -1,
    $numeric: by === "position"
  };
}
</script>
<style lang="scss" scoped>
nav {
  margin-top: 1.5em;
  margin-right: 4em;

  .menu {
    font-size: 90%;
    display: block;
    text-decoration: none;
    color: $color-passive;
    margin-bottom: 0.25em;

    &:not([href]), &:hover {
      color: $color-brand;
    }
  }

  .top {
    font-weight: bold;
  }
}
</style>
