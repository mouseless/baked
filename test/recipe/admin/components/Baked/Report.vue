<template>
  <div class="w-[1000px] mx-auto my-4">
    <Panel :header="schema.title">
      <div class="grid grid-cols-2 gap-4">
        <component
          :is="prop.component.$type"
          v-for="prop in props"
          :key="prop.name"
          :schema="prop.component"
          :data="data[prop.name]"
        />
      </div>
    </Panel>
  </div>
</template>
<script setup>
const { schema, data } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, required: true }
});

const { props } = schema;
</script>
