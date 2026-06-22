<template>
  <div class="border-b-2 border-[color:var(--color-bg-soft)]">
    <header
      class="
        flex m-auto flex-row items-center justify-between
        max-w-[82%]
        box-border
        min-w-[var(--page-min)]
        xl:max-w-[var(--max-content-width-xl)]
        lg:max-w-[var(--max-content-width-lg)]
        md:max-w-[var(--max-content-width-md)]
        sm:max-w-[var(--max-content-width-sm)]
      "
    >
      <div class="logo my-[var(--space-sm)] mx-0">
        <NuxtLink to="/" class="block h-6 xl:h-[20px]">
          <img class="baked logo inline-block h-6 xl:h-[20px]">
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
        :class="{ 'block': menuShown, 'hidden': !menuShown }"
        class="
          fixed top-0 right-0 z-[99]
          bg-[color:var(--color-bg)]
          h-full w-[calc(var(--page-min)-var(--space-md))]
          p-5 border-l-2 border-[color:var(--color-bg-second)]
          md:static md:flex md:flex-row md:items-center md:gap-[var(--space-md)]
          md:bg-transparent md:h-auto md:w-auto md:p-0 md:border-0
        "
      >
        <a
          class="close block md:hidden mb-[var(--space-sm)] h-[2em]"
          @click="toggle"
        ><i class="fa-solid fa-close text-lg" /></a>
        <NuxtLink
          v-for="menu in menus"
          :key="menu.title"
          :to="menu.path"
          :class="{
            'md:border-b-2 md:border-[color:var(--color-logo-mark)] md:pb-[calc(var(--space-sm)+2px)] md:xl:pb-[calc(var(--space-sm)+1px)]': menu.path === root,
            'max-md:border-l-2 max-md:border-l-[color:var(--color-brand)] max-md:pb-0 max-md:pl-[calc(20px+var(--space-sm))] max-md:ml-[-22px]': menu.path === root
          }"
          class="block h-[2em] m-[var(--space-sm)] no-underline last:mr-0"
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
          class="text-[color:var(--color-fg-second)] cursor-pointer"
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
  nav {
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
    }
  }
}
</style>
