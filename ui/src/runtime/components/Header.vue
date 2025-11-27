<template>
  <header
    :class="{ 'mb-4': shown || loading }"
    class="mt-4"
  >
    <Loading :skeleton="{ height: '1.28rem', width: '15rem' }">
      <Breadcrumb
        v-if="data && shown"
        :home="sitemap['/']"
        :model="parts"
        class="!bg-inherit text-sm !p-0"
      >
        <template #item="{ item }">
          <RouterLink
            :to="item.route"
            class="p-breadcrumb-item-link"
          >
            <span
              v-if="item.icon"
              :class="[item.icon, 'p-breadcrumb-item-icon']"
            />
            <span
              v-if="item.title"
              class="p-breadcrumb-item-label max-sm:truncate"
            >{{ l(item.title) }}</span>
          </RouterLink>
        </template>
      </Breadcrumb>
    </Loading>
  </header>
</template>
<script setup>
import { computed } from "vue";
import { RouterLink } from "vue-router";
import { Breadcrumb } from "primevue";
import { useContext, useLocalization } from "#imports";
import { Loading } from "#components";

const context = useContext();
const { localize: l } = useLocalization();

const { schema, data } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, required: true }
});

const { sitemap } = schema;

const loading = context.injectLoading();
const parts = computed(() => {
  if(!data) { return []; }

  const result = [];

  let page = findItem(data.path);
  while(page) {
    result.splice(0, 0, page);

    page = findItem(page.parentRoute);
  }

  return result;
});
const shown = computed(() => data?.path !== "/" && parts.value.length > 0);

function findItem(route) {
  if(sitemap[route]) { return sitemap[route]; }

  for(const key in sitemap) {
    const expression = key.replaceAll(/[{][\w\d\-:]*[}]/g, "[\\w\\d\\-]*");
    const matcher = new RegExp(`^${expression}$`, "g");

    if(matcher.test(route)) {
      return {
        ...sitemap[key],
        route
      };
    }
  };
}
</script>
