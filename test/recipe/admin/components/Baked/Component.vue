<template>
  <component
    :is="is"
    v-if="loaded"
    :schema="descriptor.schema"
    :data="data"
  />
</template>
<script setup>
const { descriptor } = defineProps({
  descriptor: { type: null, required: true }
});

const { public: { apiBaseURL: baseURL } } = useRuntimeConfig();
const resolver = useBakedComponentResolver();
const extensions = useStringExtensions();

const routeParams = inject("routeParams");

const is = resolver.resolve(descriptor.type, "Fallback");
const data = ref();
const loaded = ref(false);

onMounted(async() => {
  data.value = await fetchData();
  loaded.value = true;
});

async function fetchData() {
  if(descriptor.data?.type !== "Remote") { return descriptor.data?.value ?? descriptor.data; }

  return await $fetch(
    extensions.format(`${descriptor.data.path}`, routeParams.slice(1)),
    {
      baseURL,
      headers: { Authorization: "token-jane" }
    }
  );
}
</script>
