<template>
  <Baked.Component
    v-if="schema"
    :schema="schema"
  />
</template>
<script setup>

const { params } = defineProps({
  params: {
    type: null,
    required: true
  }
});
const schema = ref();

provide("params", params);

onMounted(async() => {
  const schemaType = params[0] ?? "index";
  schema.value = await import(`../../.baked/${schemaType}.json`)
    .catch(_ => {
      throw createError({
        statusCode: 404,
        statusMessage: `'${params[0]}' Page Not Found`,
        fatal: true
      });
    });
});
</script>
