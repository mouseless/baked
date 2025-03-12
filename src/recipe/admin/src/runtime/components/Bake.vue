<template>
  <component
    :is="is"
    :schema="descriptor.schema"
    :data="data"
    :loading="loading"
  >
    <slot v-if="$slots.default" />
  </component>
</template>
<script setup>
import { inject, onMounted, ref } from "vue";
import { useRuntimeConfig } from "#app";
import { useComponentResolver, useDataFetcher } from "#imports";

const { descriptor } = defineProps({
  descriptor: { type: null, required: true }
});

const { public: { components } } = useRuntimeConfig();
const componentResolver = useComponentResolver();
const dataFetcher = useDataFetcher();

const routeParams = inject("routeParams", []);

const is = componentResolver.resolve(descriptor.type, "None");
const shouldLoad = dataFetcher.shouldLoad(descriptor.data?.type);
const data = ref(dataFetcher.get(descriptor.data));
const loading = ref(shouldLoad);

const fetchOptions = components?.Bake?.retryFetch
  ? {
    retry: Number.MAX_VALUE,
    retryDelay: 100,
    retryStatusCodes: [500]
  }
  : { };

onMounted(async() => {
  if(!shouldLoad) { return; }

  data.value = await dataFetcher.fetch({
    baseURL: components?.Bake?.baseURL,
    data: descriptor.data,
    routeParams,
    options: fetchOptions
  });
  loading.value = false;
});
</script>
