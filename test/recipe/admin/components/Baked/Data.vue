<template>
  <div>
    <span>{{ schema.title }}</span>
    <span>
      <component
        :is="schema.component.$type"
        :schema="schema.component"
        :data="data"
      />
    </span>
  </div>
</template>
<script setup>
const { schema, data } = defineProps({
  schema: { required: true },
  data: { required: true }
});
</script>
