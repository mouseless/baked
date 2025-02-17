<template>
  <component
    :is="is"
    v-if="loaded"
    :schema="schema.$schema"
    :data="data"
  />
</template>
<script setup>
const { schema } = defineProps({
  schema: { type: null, required: true }
});

const { public: { apiBaseURL: baseURL } } = useRuntimeConfig();
const resolver = useBakedComponentResolver();
const extensions = useStringExtensions();

const routeParams = inject("routeParams");

const is = resolver.resolve(schema.$type, "Fallback");
const data = ref();
const loaded = ref(false);

onMounted(async() => {
  data.value = await fetchData();
  loaded.value = true;
});

async function fetchData() {
  if(!schema.$data?.$path) { return schema.$data; }

  return await $fetch(
    extensions.format(`${schema.$data.$path}`, routeParams.slice(1)),
    {
      baseURL,
      headers: { Authorization: "token-jane" }
    }
  );
}
</script>
