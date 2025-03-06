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

const { public: { components } } = useRuntimeConfig();
const componentResolver = useComponentResolver();
const composableResolver = useComposableResolver();
const extensions = useStringExtensions();

const routeParams = inject("routeParams", []);

const is = componentResolver.resolve(descriptor.type, "None");
const data = ref();
const loaded = ref(false);

onMounted(async() => {
  data.value = await fetchData(descriptor.data);
  loaded.value = true;
});

const fetchOptions = {
  retry: Number.MAX_VALUE,
  retryDelay: 100,
  retryStatusCodes: [500]
};

async function fetchData(data) {
  if(data?.type === "Remote") {
    const headers = data.headers ? await fetchData(data.headers) : { };

    return await $fetch(
      extensions.format(`${data.path}`, routeParams.slice(1)),
      {
        ... components?.Bake?.retryFetch ? fetchOptions : { },
        baseURL: components?.Bake?.baseURL,
        headers
      }
    );
  }

  if(data?.type === "Computed") {
    const composable = await composableResolver.resolve(data.composable);

    return composable.default();
  }

  if(data?.type === "Inline") {
    return data?.value;
  }

  return data;
}
</script>
