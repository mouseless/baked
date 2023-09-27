<template>
  <div class="top">
    <header>
      <div class="logo">
        <NuxtLink to="/">
          <img class="do logo">
        </NuxtLink>
      </div>
      <a
        class="bars"
        @click="toggle"
      ><i class="fa-solid fa-bars" /></a>
      <nav :class="{ active: menuShown }">
        <a
          class="close"
          @click="toggle"
        ><i class="fa-solid fa-close" /></a>
        <NuxtLink
          v-for="menu in menus"
          :key="menu.title"
          :to="menu._path"
          :class="{ active: menu._path === root }"
          @click="close"
        >
          {{ menu.title }}
        </NuxtLink>
        <NuxtLink
          :to="`https://github.com${runtimeConfig.public.githubURL}`"
          target="_blank"
          @click="close"
        >
          <i class="fa-brands fa-github" />
        </NuxtLink>
      </nav>
    </header>
  </div>
</template>
<script lang="ts" setup>
import { useRoute, useRuntimeConfig, ref } from "#imports";
import { useSectionStore } from "~/store/sectionStore";

const runtimeConfig = useRuntimeConfig();
const route = useRoute();
const store = useSectionStore();

const menuShown = ref<boolean>(false);
const root = computed(() => `/${route.path.split("/")[1]}`);

const menus: any = { ...store.sections };

function toggle() { menuShown.value = !menuShown.value; }
function close() { menuShown.value = false; }
</script>
<style lang="scss" scoped>
div.top {
  @include border(bottom);
}

header {
  @include width;

  margin: auto;
  display: flex;
  flex-direction: row;
  align-items: center;
  justify-content: space-between;

  .bars {
    display: none;
  }
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

a {
  @include theme("color", $theme-color-fg-passive);
  cursor: pointer;

  &:hover {
    @include theme("color", $theme-color-brand);
  }
}

nav a {
  margin: 10px;
  text-decoration: none;

  &.close {
    display: none;
  }

  &.active {
    @include border(bottom);
    @include theme("border-bottom-color", $theme-color-brand);

    padding-bottom: 22px;
  }
}

@media (max-width: $width-page-l) {
  nav {
    a {
      font-size: 0.85em;
    }
  }
}

@media (max-width: $width-page-m) {
  a.bars {
    display: block;
  }

  nav {
    @include theme("background-color", $theme-color-bg);
    @include theme("border-left-color", $theme-color-border);

    position: fixed;
    top: 0px;
    right: 0;
    z-index: 99;
    height: 100%;
    width: calc($width-page-min - 4em);
    padding: 20px;
    border-left: solid 2px;
    display: none;

    &.active {
      display: block;
    }

    a {
      display: block;
      height: 2em;

      &.close {
        display: block;
        margin-bottom: 1em;

        i {
          font-size: larger;
        }
      }

      &.active {
        border: 0;
        @include border(left);
        @include theme("border-left-color", $theme-color-brand);

        padding-bottom: 0;
        padding-left: 29px;
        margin-left: -22px;
      }
    }
  }
}

@media (max-width: $width-page-s) {
  nav {
    width: calc($width-page-min - 7em);
    a {
      font-size: 0.8em;
    }
  }
}
</style>
