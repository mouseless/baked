<template>
  <div class="border-b-2 border-bg-soft">
    <header
      class="
        flex flex-row items-center justify-between
        box-border mx-auto
        min-w-page-min max-w-page-xl
        max-xl:max-w-page-xl
        max-lg:max-w-page-l
        max-md:max-w-page-m
        max-sm:max-w-page-s
      "
    >
      <div class="logo my-sm mx-0">
        <NuxtLink to="/" class="h-6">
          <img class="baked logo h-5 xl:h-6">
        </NuxtLink>
      </div>
      <div
        v-if="menuShown"
        class="
          overlay fixed block md:hidden
          w-full h-full
          bg-darkgreen-900 opacity-50
          z-[98] left-0 top-0
        "
        @click="close"
      />
      <a
        class="bars block md:hidden"
        @click="toggle"
      ><i class="fa-solid fa-bars" /></a>
      <nav
        :class="menuShown ? 'max-md:block': 'max-md:hidden'"
        class="
          max-md:fixed max-md:top-0 max-md:right-0 max-md:z-[99]
          max-md:bg-bg
          max-md:h-full max-md:w-nav-side
          max-md:p-5 max-md:border-l-2 max-md:border-bg-second
        "
      >
        <a
          class="hidden m-sm h-[2em] max-md:block"
          @click="toggle"
        ><i class="fa-solid fa-close text-lg" /></a>
        <NuxtLink
          v-for="menu in menus"
          :key="menu.title"
          :to="menu.path"
          :class="{
            'md:border-b-2 md:pb-(--link-active-pb-md) xl:pb-(--link-active-pb-xl)': menu.path === root,
            'max-md:border-l-2 max-md:pb-0 max-md:pl-(--link-active-pl-max-md) max-md:ml-[-22px]': menu.path === root
          }"
          class="
            max-md:block no-underline
            h-[2em] md:mx-sm max-md:m-sm
            border-brand
          "
          @click="close"
        >
          {{ menu.title }}
        </NuxtLink>
        <NuxtLink
          :to="`https://github.com${runtimeConfig.public.githubURL}`"
          class="md:mx-sm max-md:m-sm max-md:block"
          target="_blank"
          @click="close"
        >
          <i class="fa-brands fa-github" />
        </NuxtLink>
        <NuxtLink
          :to="`https://matrix.to/#/${runtimeConfig.public.matrixURL}`"
          target="_blank"
          class="
            text-fg-second cursor-pointer
            md:ml-sm max-md:m-sm max-md:block
          "
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
