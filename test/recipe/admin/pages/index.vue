<template>
  <Report
    v-if="data"
    :schema="{
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
const data = ref();

onMounted(async () => {
 data.value = await $fetch("http://localhost:5151/rich-transient-with-datas/test",
   { headers: { "Authorization": "token-jane"}
 });
})
</script>
