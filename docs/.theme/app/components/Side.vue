<template>
  <nav>
    <h4><a @click="toggle">Pages</a></h4>
    <ul :class="{ active: shown }">
      <li
        v-for="menu in menus"
        :key="menu.title"
      >
        <NuxtLink
          :to="menu.path"
          :class="{ active: menu.path == $route.path }"
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
nav {
  position: sticky;
  align-self: start;
  top: $space-sm;
  margin-top: $space-md;

  h4 {
    display: none;
  }

  ul {
    margin: 0;
    padding: 0;
    max-height: calc(100vh - 8rem);
    overflow-y: auto;

    &::-webkit-scrollbar {
      display: none;
    }

    li {
      margin: 0;
      list-style: none;

      &:not(:first-child) {
        a::before {
          content: "> ";
          color: $color-brand;
          font-weight: bold;
        }
      }

      a {
        font-size: .9em;
        display: block;
        text-decoration: none;
        color: $color-fg-second;
        border-radius: $space-xs;
        padding: $space-xs $space-sm;

        &:hover {
          color: $color-brand;
        }

        &.active {
          background-color: $color-bg-nav-active;
        }
      }
    }
  }
}

@media (max-width: $width-page-m) {
  nav {
    margin-top: 0;
    width: 50%;
    top: 0;

    h4 {
      display: block;
      text-transform: uppercase;
      font-size: 0.9em;

      a {
        padding-left: 0;

        &:hover {
          color: $color-brand;
        }
      }
    }

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
