<template>
  <div class="border-b-2 border-[color:var(--color-bg-soft)]">
    <header
      class="
        flex flex-row items-center justify-between
        box-border mx-auto
        min-w-(--width-page-min) max-w-(--width-page-xl)
        max-xl:max-w-(--width-page-xl)
        max-lg:max-w-(--width-page-l)
        max-md:max-w-(--width-page-m)
        max-sm:max-w-(--width-page-s)
      "
    >
      <div class="logo my-[var(--space-sm)] mx-0">
        <NuxtLink to="/" class="h-6">
          <img class="baked logo h-5 xl:h-6">
        </NuxtLink>
      </div>
      <div
        v-if="menuShown"
        class="
          overlay fixed block md:hidden
          w-full h-full
          bg-[color:var(--color-darkgreen-900)] opacity-50
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
          max-md:bg-(color:--color-bg)
          max-md:h-full max-md:w-(--width-nav-side)
          max-md:p-5 max-md:border-l-2 max-md:border-(color:--color-bg-second)
        "
      >
        <a
          class="hidden m-(--space-sm) h-[2em] max-md:block"
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
            h-[2em] md:mx-(--space-sm) max-md:m-(--space-sm)
            border-(color:--color-brand)
          "
          @click="close"
        >
          {{ menu.title }}
        </NuxtLink>
        <NuxtLink
          :to="`https://github.com${runtimeConfig.public.githubURL}`"
          class="md:mx-(--space-sm) max-md:m-(--space-sm) max-md:block"
          target="_blank"
          @click="close"
        >
          <i class="fa-brands fa-github" />
        </NuxtLink>
        <NuxtLink
          :to="`https://matrix.to/#/${runtimeConfig.public.matrixURL}`"
          target="_blank"
          class="
            text-(color:--color-fg-second) cursor-pointer
            md:ml-(--space-sm) max-md:m-(--space-sm) max-md:block
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
