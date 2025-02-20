<template>
  <Panel :header="schema.title">
    <div v-if="schema.header" class="w-full">
      <Baked.Component :descriptor="schema.header" />
    </div>
    <div v-if="data" class="grid grid-cols-2 gap-4">
      <div
        v-for="prop in schema.props"
        :key="prop.key"
        class="flex gap-2"
      >
        <div>
          <strong>{{ prop.title }}:</strong>
        </div>
        <Baked.Component
          :descriptor="{
            ...prop.component,
            'data': data[prop.key]
          }"
          class="w-full"
        />
      </div>
    </div>
  </Panel>
  <Panel v-if="schema.tables">
    <Panel v-for="table in schema.tables" :key="table.schema.title" :header="table.schema.title">
      <Baked.Component :descriptor="table"/>
    </Panel>
  </Panel>
</template>
<script setup>
const { schema, data } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, required: true }
});
</script>
