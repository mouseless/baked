<template>
  <div v-if="prev != null || next != null" class="navigation-buttons-container">
    <div v-if="prev != null" class="button left">
      <NuxtLink :to="prev.path">
        <div class="link-text">
          <i class="fa-solid fa-caret-left" /> Previous
          <h3>
            {{ prev?.title }}
          </h3>
        </div>
      </NuxtLink>
    </div>
    <div v-if="next != null" class="button right">
      <NuxtLink :to="next?.path">
        <div class="link-text">
          Next <i class="fa-solid fa-caret-right" />
          <h3>
            {{ next?.title }}
          </h3>
        </div>
      </NuxtLink>
    </div>
  </div>
</template>
<script setup>
import { useRoute } from "#imports";
import { usePageStore } from "~/store/pageStore";

const route = useRoute();
const store = usePageStore();

const menus = store.pages;

let currentPageNumber = 0;
menus.forEach((menu, index) => {
  if(menu.path === route.path) {
    currentPageNumber = index;
  }
});

const prev = currentPageNumber > 0 ? menus[currentPageNumber - 1] : null;
const next = currentPageNumber < menus.length + 1 ? menus[currentPageNumber + 1] : null;
</script>
<style lang="scss" scoped>
.navigation-buttons-container {
  max-width: $width-content;
  margin-top: $space-md;

  .button {
    a {
      text-decoration: none;

      .link-text {
        color: $color-fg-third;
        font-size: 0.75em;

        h3 {
          color: $color-fg-second;
          margin-top: 0;
          font-size: 1.5em;
        }
      }

      &:hover {
        h3 {
          color: $color-brand;
        }
      }
    }

    &.left {
      text-align: left;
      float: left;
    }

    &.right {
      text-align: end;
      float: right;
    }
  }
}
</style>
