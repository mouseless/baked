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

const components = import.meta.glob("~/components/*/*.vue");

const is = computed(() =>
  components[`/components/Baked/${schema.$type}.vue`]
    ? defineAsyncComponent(components[`/components/Baked/${schema.$type}.vue`])
    : defineAsyncComponent(components["/components/Baked/Fallback.vue"])
);
</script>
