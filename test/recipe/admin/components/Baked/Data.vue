<template>
  <div class="flex gap-2">
    <div class="max-w-10">
      <strong>{{ schema.title }}</strong>
    </div>
    <component
      :is="schema.component.$type"
      :schema="schema.component"
      :data="data"
      class="w-full"
    />
  </div>
</template>
<script setup>
const { schema, data } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, required: true }
});
</script>
