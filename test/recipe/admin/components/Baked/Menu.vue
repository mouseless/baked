<template>
  <Menubar :model="items" />
</template>
<script setup>
const { schema, data } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, required: true }
});

const items = computed(() => [
  {
    label: schema.title,
    icon: schema.icon,
    items: data.map(item => {
      return {
        label: item.title,
        url: item.path
      };
    })
  }
]);
</script>
