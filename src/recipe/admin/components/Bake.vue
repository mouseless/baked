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
import useComponentResolver from "../composables/useComponentResolver.mjs";
import useComposableResolver from "../composables/useComposableResolver.mjs";
import useStringExtensions from "../composables/useStringExtensions.mjs";

const { descriptor } = defineProps({
  descriptor: { type: null, required: true }
});

const { public: { apiBaseURL: baseURL, devMode } } = useRuntimeConfig();
const component = useComponentResolver();
const composable = useComposableResolver();
const extensions = useStringExtensions();

const routeParams = inject("routeParams", []);

const is = component.resolve(descriptor.type, "None");
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
    return composable.resolve(descriptor.data.composable)();
  }

  if(descriptor.data?.type === "Inline") {
    return descriptor.data?.value;
  }

  return descriptor.data;
}
</script>
