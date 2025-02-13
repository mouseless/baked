<template>
  <component
    :is="schema.$type"
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
  schema.value = await import(`../../.baked/${params[0]}.json`)
    .catch(_ => {
      throw createError({
        statusCode: 404,
        statusMessage: `'${params[0]}' Page Not Found`,
        fatal: true
      });
    });
});
</script>
