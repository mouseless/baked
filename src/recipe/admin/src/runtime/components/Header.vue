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
import { computed } from "vue";
import { RouterLink } from "vue-router";
import { Breadcrumb } from "primevue";

const { schema, data } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, required: true }
});

const parts = computed(() => {
  const result = [];

  let page = findItem(data.path);
  while(page) {
    result.splice(0, 0, page);

    page = findItem(page.parentRoute);
  }

  return result;
});
const shown = computed(() => data.path !== "/" && parts.value.length > 0);

function linkOrSpan(item) {
  if(data.path !== item.route) {
    return RouterLink;
  }

  return "span";
}

function findItem(route) {
  if(schema.sitemap[route]) { return schema.sitemap[route]; }

  for(const key in schema.sitemap){
    const expression = key.replaceAll(/[{][\w\d\-:]*[}]/g, "[\\w\\d\-]*");
    const matcher = new RegExp(`^${expression}$`, "g");

    if(matcher.test(route)) {
      return {
        ...schema.sitemap[key],
        route
      };
    }
  };
}
</script>
