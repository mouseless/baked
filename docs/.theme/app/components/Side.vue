<template>
  <nav
    class="
      c--side sticky self-start top-sm mt-md
      max-md:mt-0 max-md:w-1/2 max-md:top-0
    "
  >
    <h4 class="md:hidden block uppercase text-[0.9em]">
      <a class="pl-0! hover:text-brand!" @click="toggle">Pages</a>
    </h4>
    <ul
      :class="{ active: shown }"
      class="
        m-0 p-0 overflow-y-auto
        [&::-webkit-scrollbar]:hidden
        max-h-[calc(100vh-8rem)]
      "
    >
      <li
        v-for="menu in menus"
        :key="menu.title"
        class="m-0! list-none"
      >
        <NuxtLink
          :to="menu.path"
          :class="{ 'bg-bg-nav-active': menu.path == $route.path }"
          class="
            block rounded-xs no-underline
            text-fg-second text-[0.9em]
            hover:text-brand
            px-sm py-xs
          "
          @click="close"
        >
          {{ menu.title }}
        </NuxtLink>
      </li>
    </ul>
  </nav>
</template>
<script setup>
import { watch, ref } from "#imports";
import { usePageStore } from "~/store/pageStore";

const store = usePageStore();

const shown = ref(false);
const menus = ref(store.pages);

watch(usePageStore(), () => {
  menus.value = store.pages;
});

function close() { shown.value = false; }
function toggle() { shown.value = !shown.value; }
</script>
<style lang="scss" scoped>
@media (max-width: $width-page-m) {
  nav {
    ul {
      display: none;
      position: absolute;
      width: 200%;
      background-color: $color-bg-nav;
      border-radius: $space-sm;
      padding: $space-sm;
      box-sizing: border-box;
      max-height: calc(100vh - 10rem);

      &.active {
        display: block;
      }

      li a {
        &.active {
          background-color: $color-bg-third;
        }
      }
    }
  }
}

@media (max-width: $width-page-s) {
  nav {
    margin-bottom: -54px;
  }
}
</style>
