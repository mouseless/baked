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
  color: $color-fg-second;
  cursor: pointer;

  &:hover {
    color: $color-brand;
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

    border-bottom-color: $color-bg-third;
    padding-bottom: 22px;
  }

  &:last-child {
    margin-right: 0;
  }
}

@media (max-width: $width-page-xl) {
  nav a.active {
    padding-bottom: 24px;
  }
}

@media (max-width: $width-page-m) {
  a.bars {
    display: block;
  }

  nav {
    position: fixed;
    top: 0px;
    right: 0;
    z-index: 99;
    background: $color-bg;
    height: 100%;
    width: calc($width-page-min - 4em);
    padding: 20px;
    border-left: solid 2px $color-bg-second;
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
        border-left-color: $color-bg-third;

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
  }
}
</style>
