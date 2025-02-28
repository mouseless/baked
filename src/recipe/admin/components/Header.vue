<template>
  <header
    :class="{ 'mb-4': shown }"
    class="mt-4"
  >
    <Breadcrumb
      v-if="shown"
      :home="schema.sitemap['/']"
      :model="parts"
      class="!bg-inherit text-sm !p-0"
    >
      <template #item="{ item }">
        <component
          :is="linkOrSpan(item)"
          :to="data.path !== item.route ? item.route : '#'"
          class="p-breadcrumb-item-link"
        >
          <span
            v-if="item.icon"
            :class="[item.icon, 'p-breadcrumb-item-icon']"
          />
          <span
            v-if="item.title"
            class="p-breadcrumb-item-label"
          >{{ item.title }}</span>
        </component>
      </template>
    </Breadcrumb>
  </header>
</template>
<script setup>
import { RouterLink } from "vue-router";
import { Breadcrumb } from "primevue";

const { schema, data } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, required: true }
});

const shown = computed(() => data.path !== "/");
const parts = computed(() => {
  const result = [];

  let page = schema.sitemap[data.path];
  while(page) {
    result.splice(0, 0, page);

    page = schema.sitemap[page.parentRoute];
  }

  return result;
});

function linkOrSpan(item) {
  if(data.path !== item.route) {
    return RouterLink;
  }

  return "span";
}
</script>
