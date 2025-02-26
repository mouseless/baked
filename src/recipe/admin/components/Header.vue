<template>
  <header
    :class="{ 'mb-4': shown }"
    class="mt-4"
  >
    <Breadcrumb
      v-if="shown"
      :home="menu['/']"
      :model="parts"
      class="!bg-inherit text-sm !p-0"
    >
      <template #item="{ item }">
        <RouterLink
          :to="$route.path !== item.route ? item.route : ''"
          class="p-breadcrumb-item-link"
        >
          <span :class="[item.icon, 'p-breadcrumb-item-icon']" />
          <span class="p-breadcrumb-item-label">{{ item.title }}</span>
        </RouterLink>
      </template>
    </Breadcrumb>
  </header>
</template>
<script setup>
import { RouterLink } from "vue-router";
import { Breadcrumb } from "primevue";
import usePage from "../composables/usePage.mjs";

const page = usePage();
const { public: { menu } } = useRuntimeConfig();

const shown = computed(() => page.route !== "/");
const parts = computed(() => {
  return shown.value
    ? !page.parent
      ? [page]
      : [page.parent, page]
    : [];
});

</script>
