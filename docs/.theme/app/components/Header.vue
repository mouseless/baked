<template>
  <div class="top">
    <header class="flex m-auto flex-row items-center justify-between">
      <div class="logo my-[var(--space-sm)] mx-0">
        <NuxtLink to="/" class="block h-6">
          <img class="baked logo inline-block h-6">
        </NuxtLink>
      </div>
      <div
        v-if="menuShown"
        class="
          overlay fixed hidden
          w-full h-full
          bg-[color:var(--color-darkgreen-900)] opacity-50
          z-[98] m-0 p-0 left-0 top-0
        "
        @click="close"
      />
      <a
        class="bars hidden"
        @click="toggle"
      ><i class="fa-solid fa-bars" /></a>
      <nav :class="{ active: menuShown }">
        <a
          class="close hidden"
          @click="toggle"
        ><i class="fa-solid fa-close" /></a>
        <NuxtLink
          v-for="menu in menus"
          :key="menu.title"
          :to="menu.path"
          :class="{
            'border-b-2 border-[color:var(--color-logo-mark)] pb-[calc(var(--space-sm)+2px)]': menu.path === root
          }"
          class="m-[var(--space-sm)] no-underline last:mr-0"
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
        <NuxtLink
          :to="`https://matrix.to/#/${runtimeConfig.public.matrixURL}`"
          target="_blank"
          class="matrix"
          @click="close"
        >
          <Icon.Matrix />
        </NuxtLink>
      </nav>
    </header>
  </div>
</template>
<script setup>
import { useRoute, useRuntimeConfig, ref } from "#imports";
import { useSectionStore } from "~/store/sectionStore";

const runtimeConfig = useRuntimeConfig();
const route = useRoute();
const store = useSectionStore();

const menuShown = ref(false);
const root = computed(() => `/${route.path.split("/")[1]}`);

const menus = { ...store.sections };

function toggle() { menuShown.value = !menuShown.value; }
function close() { menuShown.value = false; }
</script>
<style lang="scss" scoped>
div.top {
  @include border(bottom);
}

header {
  @include width;
}

@media (max-width: $width-page-xl) {
  div.logo {
    a:has(img.logo) {
      height: 20px;

      img.baked {
        &:is(.logo) {
          height: 20px;
        }
      }
    }
  }
  nav a.active {
    padding-bottom: calc($space-sm + 1px);
  }
}

@media (max-width: $width-page-m) {
  a.bars {
    display: block;
  }

  .overlay {
    display: block;
  }

  nav {
    position: fixed;
    top: 0;
    right: 0;
    z-index: 99;
    background: $color-bg;
    height: 100%;
    width: calc($width-page-min - $space-md);
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
        margin-bottom: $space-sm;

        i {
          font-size: larger;
        }
      }

      &.active {
        border: 0;
        @include border(left);
        border-left-color: $color-brand;

        padding-bottom: 0;
        padding-left: calc(20px + $space-sm);
        margin-left: -22px;
      }
    }
  }
}
</style>
