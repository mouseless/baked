<template>
  <div class="navigation-buttons">
    <div class="prev-button">
      <NuxtLink v-if="prev != null" :to="prev?._path">
        {{ prev?.title }}
      </NuxtLink>
    </div>
    <div class="next-button">
      <NuxtLink v-if="next != null" :to="next?._path">
        {{ next?.title }}
      </NuxtLink>
    </div>
  </div>
</template>
<script lang="ts" setup>
import { useRoute } from "#imports";

const route = useRoute();
const root = `/${route.path.split("/")[1]}`;

console.log(root);
console.log(route);

const index = await queryContent(root)
  .where({ _path: { $eq: root } })
  .only(["_path", "title", "position"])
  .findOne();

const sections = await queryContent(root)
  .where({ _path: { $ne: root } })
  .only(["_path", "title", "position"])
  .sort(sorter(index.sort))
  .find();

const menus = root === "/" ? [index] : [index, ...sections];

console.log(menus);

let currentPageNumber = 0;
menus.forEach((menu, index) => {
  if(menu._path === route.path) {
    currentPageNumber = index;
  }
});

const prev = currentPageNumber > 0 ? menus[currentPageNumber - 1] : null;
const next = currentPageNumber < menus.length + 1 ? menus[currentPageNumber + 1] : null;

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
.navigation-buttons
{
  display: flex;
  flex-direction: row;
  justify-content: space-between;
}
</style>
