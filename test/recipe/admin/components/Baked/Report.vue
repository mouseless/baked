<template>
  <component
    v-for="prop in props"
    :is="prop.component.$type"
    :schema="prop.component"
    :data="data[prop.name]"
  />
</template>
<script setup>
const { schema, data } = defineProps({
  schema: { required: true },
  data: { required: true }
});

const { props } = schema;
</script>
