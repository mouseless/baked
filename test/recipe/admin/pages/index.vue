<template>
  <Report
    v-if="data"
    :schema="{
      title: 'Rich Transient w/ Data',
      props: [
        {
          name: 'id',
          component: {
            $type: 'Data',
            title: 'ID',
            component: {
              $type: 'String'
            }
          }
        },
        {
          name: 'time',
          component: {
            $type: 'String'
          }
        }
      ]
    }"
    :data="data"
  />
</template>
<script setup>
const { public: { apiBaseURL: baseURL } } = useRuntimeConfig();

const data = ref();

onMounted(async() => {
  data.value = await $fetch(
    "rich-transient-with-datas/test",
    {
      baseURL,
      headers: { Authorization: "token-jane" }
    }
  );
});
</script>
