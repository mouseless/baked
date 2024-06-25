<template>
  <div class="bottom">
    <footer>
      <div class="logo">
        <NuxtLink to="/">
          <img class="baked logo mono">
        </NuxtLink>
        <span>Copyright (c) 2024 Mouseless - MIT License</span>
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
import { useRoute, useRuntimeConfig } from "#imports";
import { useSectionStore } from "~/store/sectionStore";

const runtimeConfig = useRuntimeConfig();
const route = useRoute();
const store = useSectionStore();

const root = computed(() => `/${route.path.split("/")[1]}`);

const menus: any = { ...store.sections };
</script>
<style lang="scss" scoped>
div.bottom {
  @include border(top);

  margin-top: $space-md;
}

footer {
  @include width;

  margin: auto;
  margin-top: $space-sm;
  margin-bottom: $space-sm;
  display: flex;
  flex-direction: row;
  align-items: flex-start;
  justify-content: space-between;

  &, & * {
    color: $color-fg-third;
  }

  div, nav {
    font-size: smaller;
    font-family: $font-heading;
  }
}

div.logo {
  margin: $space-sm 0;

  a:has(img.logo) {
    display: block;
    height: 12px;

    img.baked {
      &:is(.logo) {
        height: 12px;
        display: inline-block;
        opacity: 0.5;
      }
    }
  }

  span {
    display: inline-block;
    margin-top: $space-sm;
  }
}

nav {
  text-align: right;

  a {
    margin: $space-xs;
    text-decoration: none;
    display: block;

    &:hover {
      &, & i {
        color: $color-brand;
      }
    }
  }
}

@media (max-width: $width-page-s) {
  footer {
    display: block;

    nav {
      text-align: left;

      a {
        margin-left: 0;
      }
    }
  }
}
</style>
