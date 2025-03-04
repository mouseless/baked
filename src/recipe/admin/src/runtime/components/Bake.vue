<template>
  <component
    :is="is"
    v-if="loaded"
    :schema="descriptor.schema"
    :data="data"
  >
    <slot v-if="$slots.default" />
  </component>
</template>
<script setup>
import { inject, onMounted, ref } from "vue";
import { useRuntimeConfig } from "#app";
import { useComponentResolver, useComposableResolver, useStringExtensions } from "#imports";

const { descriptor } = defineProps({
  descriptor: { type: null, required: true }
});

const { public: { apiBaseURL: baseURL, devMode } } = useRuntimeConfig();
const componentResolver = useComponentResolver();
const composableResolver = useComposableResolver();
const extensions = useStringExtensions();

const routeParams = inject("routeParams", []);

const is = componentResolver.resolve(descriptor.type, "None");
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
  if(descriptor.data?.type === "Remote") {
    return await $fetch(
      extensions.format(`${descriptor.data.path}`, routeParams.slice(1)),
      {
        ... devMode ? fetchOptions : { },
        baseURL,
        headers: { Authorization: "token-jane" }
      }
    );
  }

  if(descriptor.data?.type === "Computed") {
    const composable = await composableResolver.resolve(descriptor.data.composable);

    return composable.default();
  }

  if(descriptor.data?.type === "Inline") {
    return descriptor.data?.value;
  }

  return descriptor.data;
}
</script>
