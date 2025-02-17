<template>
  <Baked.Component
    v-if="schema"
    :schema="schema"
  />
</template>
<script setup>
const { routeParams } = defineProps({
  routeParams: {
    type: null,
    required: true
  }
});

const schema = ref();
const schemaName = computed(() => routeParams[0] ?? "index");

provide("routeParams", routeParams);

onMounted(async() => {
  schema.value = await import(`../../.baked/${schemaName.value}.json`)
    .catch(_ => {
      throw createError({
        statusCode: 404,
        statusMessage: `'${schemaName.value}' Page Not Found`,
        fatal: true
      });
    });
});
</script>
