<template>
  <component
    :is="is"
    v-if="schema"
    :schema="schema"
    :data="data"
  />
</template>
<script setup>
const { schema, data } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, required: false, default: {} }
});

const is = useBakedComponent().resolve(schema.$type, "Fallback");

</script>
