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
    ? defineAsyncComponent(
      components[`/components/Baked/${schema.$type}.vue`]
    )
    : () => {
      throw createError({
        statusCode: 404,
        statusMessage: `'${schema.$type}' Component Not Found`,
        fatal: true
      });
    }
);
</script>
