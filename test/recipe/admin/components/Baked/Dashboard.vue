<template>
  <div class="w-[1000px] mx-auto my-4">
    <Panel v-if="schema" :header="schema.title">
      <div class="grid grid-cols-2 gap-4">
        <Baked.Component
          v-for="prop in schema.props"
          :key="prop.name"
          :schema="prop.component"
        />
      </div>
    </Panel>
  </div>
</template>
<script setup>
const { schema } = defineProps({
  schema: { type: null, required: true }
});
</script>
