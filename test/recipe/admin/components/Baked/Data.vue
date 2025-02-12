<template>
  <div>
    <span>{{ schema.title }}</span>
    <span>
      <component
        :is="schema.component"
        :schema="schema"
        :data="data"
      />
    </span>
  </div>
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
</script>
