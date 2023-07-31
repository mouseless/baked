<template>
  <div v-if="prev != null || next != null" class="navigation-buttons-container">
    <div v-if="prev != null" class="button left">
      <NuxtLink :to="prev._path">
        <div class="link-text">
          <i class="fa-solid fa-caret-left" /> Previous
          <h4>
            {{ prev?.title }}
          </h4>
        </div>
      </NuxtLink>
    </div>
    <div v-if="next != null" class="button right">
      <NuxtLink :to="next?._path">
        <div class="link-text">
          Next <i class="fa-solid fa-caret-right" />
          <h4>
            {{ next?.title }}
          </h4>
        </div>
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
  .only(["_path", "title", "sections"])
  .findOne();

const sections = await queryContent(root)
  .where({ _path: { $ne: root } })
  .only(["_path", "title"])
  .find();

sections.sort((a, b) => sectionsSorter(a, b, index.sections));

const menus = root === "/" ? [index] : [index, ...sections];

let currentPageNumber = 0;
menus.forEach((menu, index) => {
  if(menu._path === route.path) {
    currentPageNumber = index;
  }
});

const prev: any = currentPageNumber > 0 ? menus[currentPageNumber - 1] : null;
const next: any = currentPageNumber < menus.length + 1 ? menus[currentPageNumber + 1] : null;
</script>
<style lang="scss" scoped>
.navigation-buttons-container {
  margin-top: 4em;

  & .button {
    padding-right: 0.4rem;
    padding-left: 0.4rem;
    margin-top: 15px;

    & a {
      text-decoration: none;
    }

    &.left {
      text-align: left;
      float: left;
    }

    &.right {
      text-align: end;
      float: right;
    }

    & .link-text {
      color: $color-fg-passive;
      font-size: 12px;

      & h4 {
        margin-top: 1px;
        font-size: 18px;
        color: $color-brand;
      }

      & i {
        color: $color-fg-passive;
      }
    }
  }
}
</style>
