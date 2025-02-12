<template>
  <Report
    :schema="{
      props: [
        {
          name: 'id',
          component: 'Data',
          schema: {
            title: 'ID',
            component: 'String'
          }
        },
        {
          name: 'time',
          component: 'Data',
          schema: {
            title: 'Time',
            component: 'String'
          }
        }
      ]
    }"
    :data="data"
  />
</template>
<script setup>
const data = ref({});

onMounted(async () => {
 data.value = await $fetch("http://localhost:5151/rich-transient-with-datas/test",
   { headers: { "Authorization": "token-jane"}
 });
})
</script>
