<template>
  <div v-if="prev != null || next != null" class="navigation-buttons-container">
    <div v-if="prev != null" class="button left">
      <NuxtLink :to="prev._path">
        <div class="link-text">
          <i class="fa-solid fa-caret-left" /> Previous
          <h3>
            {{ prev?.title }}
          </h3>
        </div>
      </NuxtLink>
    </div>
    <div v-if="next != null" class="button right">
      <NuxtLink :to="next?._path">
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
<script lang="ts" setup>
import { useRoute } from "#imports";
import { usePageStore } from "~/store/pageStore";

const route = useRoute();
const store = usePageStore();

const menus: any = store.pages;

let currentPageNumber = 0;
menus.forEach((menu:any, index:any) => {
  if(menu._path === route.path) {
    currentPageNumber = index;
  }
});

const prev: any = currentPageNumber > 0 ? menus[currentPageNumber - 1] : null;
const next: any = currentPageNumber < menus.length + 1 ? menus[currentPageNumber + 1] : null;
</script>
<style lang="scss" scoped>
.navigation-buttons-container {
  max-width: $width-content;
  margin-top: 4em;

  .button {
    padding-right: 0.4em;
    padding-left: 0.4em;
    margin-top: 0.9em;

    a {
      text-decoration: none;

      .link-text {
        color: $color-fg-third;
        font-size: 0.75em;

        h3 {
          color: $color-fg-second;
          margin-top: 0.1em;
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
