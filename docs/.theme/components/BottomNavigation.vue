<template>
  <div class="navigation-buttons">
    <div v-if="prev != null" class="button">
      <NuxtLink :to="prev?._path">
        <h4>
          <i class="fa-solid fa-caret-left"></i>
          {{ prev?.title }}
        </h4>
      </NuxtLink>
    </div>
    <div v-if="next != null" class="button">
      <NuxtLink :to="next?._path">
        <h4>
          {{ next?.title }}
          <i class="fa-solid fa-caret-right"></i>
        </h4>
      </NuxtLink>
    </div>
  </div>
</template>
<script lang="ts" setup>
import { useRoute } from "#imports";

const route = useRoute();
const root = `/${route.path.split("/")[1]}`;

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

  & .button {
    padding-right: 0.4rem;
    padding-left: 0.4rem;
    & a {
      text-decoration: none;
    }
  }
}
</style>
