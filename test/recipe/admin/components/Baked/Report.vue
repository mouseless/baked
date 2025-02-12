<template>
  <component
    v-for="prop in props"
    :is="prop.component"
    :schema="prop.schema"
    :data="data[prop.name]"
  />
</template>
<script setup>
const { schema, data } = defineProps({
  schema: {
    type: Object,
    required: true
  },
  data: {
    type: Object,
    required: true
  }
});

const { props } = schema;
</script>
