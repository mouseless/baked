<template>
  <component
    :is="is"
    v-if="loaded"
    :schema="descriptor.schema"
    :data="data"
  />
</template>
<script setup>
import { inject, onMounted, ref } from "vue";
import { useRuntimeConfig } from "#app";
import { useComponentResolver, useStringExtensions } from "#imports";

const { descriptor } = defineProps({
  descriptor: { type: null, required: true }
});

const { public: { apiBaseURL: baseURL, devMode } } = useRuntimeConfig();
const resolver = useComponentResolver();
const extensions = useStringExtensions();

const routeParams = inject("routeParams", []);

const is = resolver.resolve(descriptor.type, "None");
const data = ref();
const loaded = ref(false);

onMounted(async() => {
  data.value = await fetchData();
  loaded.value = true;
});

const fetchOptions = {
  retry: Number.MAX_VALUE,
  retryDelay: 100,
  retryStatusCodes: [500]
};

async function fetchData() {
  if(descriptor.data?.type !== "Remote") { return descriptor.data?.value ?? descriptor.data; }

  return await $fetch(
    extensions.format(`${descriptor.data.path}`, routeParams.slice(1)),
    {
      ... devMode ? fetchOptions : { },
      baseURL,
      headers: { Authorization: "token-jane" }
    }
  );
}
</script>
