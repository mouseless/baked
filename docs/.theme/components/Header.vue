<template>
  <div class="top">
    <header>
      <div class="logo">
        <NuxtLink to="/">
          <img class="do logo">
        </NuxtLink>
      </div>
      <nav>
        <ContentQuery
          v-slot="{ data: menus }"
          path="/"
          :only="[ '_path', 'title', 'position' ]"
          :where="{ _dir: { $eq: '' }, _path: { $ne: '/' }, position: { $exists: true } }"
          :sort="sort"
        >
          <NuxtLink
            v-for="menu in menus"
            :key="menu.title"
            :to="menu._path == root ? '' : menu._path"
          >
            {{ menu.title }}
          </NuxtLink>
        </ContentQuery>
        <NuxtLink
          :to="runtimeConfig.public.githubURL"
          target="_blank"
        >
          <i class="fa-brands fa-github" />
        </NuxtLink>
      </nav>
    </header>
  </div>
</template>
<script lang="ts" setup>
import { useRoute, useRuntimeConfig } from "#imports";

const runtimeConfig = useRuntimeConfig();
const route = useRoute();
const root = computed(() => `/${route.path.split("/")[1]}`);
const sort = { position: 1, $numeric: true };
</script>
<style lang="scss" scoped>
div.top {
  @include border(bottom);
}

header {
  width: $width-page;
  margin: auto;
  padding: 0 10px;
  display: flex;
  flex-direction: row;
  align-items: center;
  justify-content: space-between;
}

div.logo {
  margin: 20px 0px;

  a:has(img.logo) {
    display: block;
    height: 25px;

    img.do {
      &:is(.logo) {
        height: 25px;
        display: inline-block;
      }
    }
  }
}

nav a {
  margin: 10px;
  text-decoration: none;
  color: $color-passive;

  &:hover, &:not([href]) {
    color: $color-brand;
  }
}
</style>
