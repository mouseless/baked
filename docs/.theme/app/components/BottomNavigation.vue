<template>
  <div v-if="prev != null || next != null" class="max-w-(--width-content) mt-(--space-md)">
    <div v-if="prev != null" class="text-left float-left">
      <NuxtLink :to="prev.path" class="group no-underline">
        <div class="text-(--color-brand)">
          <span class="text-[0.8em]"><i class="fa-solid fa-caret-left" /> Previous</span>
          <h3
            class="
              mt-[-0.25em]
              text-(--color-fg-second) text-[1.125em] group-hover:text-(--color-brand)
            "
          >
            {{ prev?.title }}
          </h3>
        </div>
      </NuxtLink>
    </div>
    <div v-if="next != null" class="text-end float-right">
      <NuxtLink :to="next?.path" class="group no-underline">
        <div class="text-(--color-brand)">
          <span class="text-[0.8em]">Next <i class="fa-solid fa-caret-right" /></span>
          <h3
            class="
              mt-[-0.25em]
              text-(--color-fg-second) text-[1.125em] group-hover:text-(--color-brand)
            "
          >
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