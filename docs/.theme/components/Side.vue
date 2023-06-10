<template>
  <nav>
    <h4><a @click="toggle">Sections</a></h4>
    <ul :class="{ active: shown }">
      <li>
        <NuxtLink
          v-for="menu in menus"
          :key="menu.title"
          :to="menu._path"
          :class="{ active: menu._path == $route.path }"
          @click="close"
        >
          {{ menu.title }}
        </NuxtLink>
      </li>
    </ul>
  </nav>
</template>
<script lang="ts" setup>
import type { ParsedContent } from "@nuxt/content/dist/runtime/types";
import { computed, useRoute, watch, queryContent, ref } from "#imports";

const shown = ref<boolean>(false);
function toggle() { shown.value = !shown.value; }
function close() { shown.value = false; }

const route = useRoute();
const root = computed(() => `/${route.path.split("/")[1]}`);

const index = await queryContent(root.value)
  .where({ _path: { $eq: root.value } })
  .only(["_path", "title", "position"])
  .findOne();

const sections = await queryContent(root.value)
  .where({ _path: { $ne: root.value } })
  .only(["_path", "title", "position"])
  .sort(sorter(index.sort))
  .find();

const menus = ref<Pick<ParsedContent, string>[]>([index, ...sections]);

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
  position: sticky;
  align-self: start;
  top: 1.5em;
  margin-top: 2.5em;

  h4 {
    display: none;
  }

  ul {
    margin: 0;
    padding: 0;

    li {
      margin: 0;
      list-style: none;

      a {
        font-size: 90%;
        display: block;
        text-decoration: none;
        color: $color-fg-passive;

        border-radius: 5px;
        padding: 10px;

        &:hover {
          color: $color-brand;
        }

        &.active {
          background-color: $color-border;
        }
      }
    }
  }
}

@media (max-width: $width-page-l) {
  nav {
    margin-top: 3.5em;
  }
}

@media (max-width: $width-page-m) {
  nav {
    background-color: $color-bg-body;
    margin-top: 0;
    width: 50%;
    top: 0;
    @include radius;

    h4 {
      display: block;
      text-transform: uppercase;
      font-size: 80%;

      a {
        padding-left: 0;
      }
    }

    ul {
      display: none;
      @include box;
      position: absolute;
      width: 200%;
      padding: 0.5em;
      box-sizing: border-box;

      &.active {
        display: block;
      }
    }
  }
}
</style>
