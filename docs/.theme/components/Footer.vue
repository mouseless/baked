<template>
  <div class="bottom">
    <footer>
      <div class="logo">
        <NuxtLink to="/">
          <img class="do logo white">
        </NuxtLink>
        <span>Copyright (c) 2023 Mouseless - MIT License</span>
      </div>
      <nav>
        <NuxtLink
          v-for="menu in menus"
          :key="menu.title"
          :to="menu._path"
          :class="{ active: menu._path === root }"
        >
          {{ menu.title }}
        </NuxtLink>
        <NuxtLink
          :to="`https://github.com${runtimeConfig.public.githubURL}`"
          target="_blank"
        >
          <i class="fa-brands fa-github" /> {{ runtimeConfig.public.githubURL }}
        </NuxtLink>
      </nav>
    </footer>
  </div>
</template>
<script lang="ts" setup>
import { useRoute, useRuntimeConfig, queryContent } from "#imports";

const runtimeConfig = useRuntimeConfig();
const route = useRoute();
const root = computed(() => `/${route.path.split("/")[1]}`);
const menus = await queryContent("/")
  .only(["_path", "title", "position"])
  .where({ _dir: { $eq: "" }, _path: { $ne: "/" }, position: { $exists: true } })
  .sort({ position: 1, $numeric: true })
  .find();
</script>
<style lang="scss" scoped>
div.bottom {
  @include border(top);
  margin-top: 4em;
}

footer {
  font-size: smaller;
  max-width: $width-page;
  margin: auto;
  margin-top: 1em;
  margin-bottom: 1em;
  padding: 0 10px;
  display: flex;
  flex-direction: row;
  align-items: flex-start;
  justify-content: space-between;

  &, & * {
    color: $color-fg-passive;
  }
}

div.logo {
  margin: 20px 0px;

  a:has(img.logo) {
    display: block;
    height: 15px;

    img.do {
      &:is(.logo) {
        height: 15px;
        display: inline-block;
        opacity: 0.5;
      }
    }
  }

  span {
    display: inline-block;
    margin-top: 1em;
  }
}

nav {
  text-align: right;

  a {
    margin: 5px;
    text-decoration: none;
    display: block;

    &:hover {
      &, & i {
        color: $color-brand;
      }
    }
  }
}
</style>
