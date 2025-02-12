<template>
  <component
    :is="schema.$type"
    v-if="data"
    :schema="schema"
    :data="data"
  />
</template>
<script setup>
import schema from "~/pages/rich-transient-with-datas.json";

const { public: { apiBaseURL: baseURL } } = useRuntimeConfig();

const data = ref();

onMounted(async() => {
  data.value = await $fetch(
    `${schema.path}/test`,
    {
      baseURL,
      headers: { Authorization: "token-jane" }
    }
  );
});
</script>
