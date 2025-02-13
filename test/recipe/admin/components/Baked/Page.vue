<template>
  <component
    :is="schema.$type"
    v-if="data"
    :schema="schema"
    :data="data"
  />
</template>
<script setup>
const { params } = defineProps({
  params: {
    type: null,
    required: true
  }
});

const { public: { apiBaseURL: baseURL } } = useRuntimeConfig();

const schema = ref();
const data = ref();

onMounted(async() => {
  schema.value = await import(`../../.baked/${params[0]}.json`);

  data.value = await $fetch(
    `${schema.value.path}/${params[1]}`,
    {
      baseURL,
      headers: { Authorization: "token-jane" }
    }
  );
});
</script>
