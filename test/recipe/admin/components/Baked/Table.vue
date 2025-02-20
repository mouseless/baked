<template>
  <DataTable :value="items">
    <Column v-for="col of columns" :key="col.field" :field="col.field" :header="col.header"/>
  </DataTable>
</template>
<script setup>
const { schema, data } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, required: true }
});

const items = ref(data);
const columns = ref(schema.columns);
</script>