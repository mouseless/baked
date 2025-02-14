<template>
  <div class="w-[1000px] mx-auto my-4">
    <Panel v-if="schema" :header="schema.title">
      <div class="grid grid-cols-1 gap-4">
        <Baked.Component
          v-for="menu in menus"
          :key="menu.title"
          :schema="menu.component"
          :data="menu.data"
        />
      </div>
    </Panel>
  </div>
</template>
<script setup>
const { schema } = defineProps({
  schema: { type: null, required: true }
});

const { menus } = schema;
</script>
