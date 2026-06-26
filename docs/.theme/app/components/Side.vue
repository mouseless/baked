<template>
  <nav
    class="
      c--side sticky self-start top-sm mt-md
      max-md:mt-0 max-md:w-1/2 max-md:top-0
      max-sm:mb-[-62px]
    "
  >
    <h4 class="md:hidden block uppercase text-[0.9em]">
      <a class="pl-0! hover:text-brand!" @click="toggle">Pages</a>
    </h4>
    <ul
      :class="{ 'max-md:block!': shown }"
      class="
        m-0 p-0! overflow-y-auto max-h-[calc(100vh-8rem)]
        [&::-webkit-scrollbar]:hidden
        max-md:hidden max-md:absolute
        max-md:w-[200%] max-md:bg-bg-nav
        max-md:rounded-sm max-md:p-sm!
        max-md:box-border max-md:max-h-(--max-height-list)
      "
    >
      <li
        v-for="menu in menus"
        :key="menu.title"
        class="m-0! list-none"
      >
        <NuxtLink
          :to="menu.path"
          :class="{ 'bg-bg-nav-active max-md:bg-bg-third': menu.path == $route.path }"
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
