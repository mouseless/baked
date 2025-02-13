<template>
  <component
    :is="schema.$type"
    v-if="data"
    :schema="schema"
    :data="data"
  />
</template>
<script setup>
const { public: { apiBaseURL: baseURL } } = useRuntimeConfig();

const route = useRoute();
const schema = ref();
const data = ref();

onMounted(async() => {
  schema.value = await import(`../../pages/${route.params.schema}.json`);

  data.value = await $fetch(
    `${schema.value.path}/${route.params.id}`,
    {
      baseURL,
      headers: { Authorization: "token-jane" }
    }
  );
});
</script>
