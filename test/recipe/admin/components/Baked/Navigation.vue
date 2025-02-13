<template>
  <NuxtLink :to="data.path">
    {{ schema.title }}
  </NuxtLink>
</template>
<script setup>
import { NuxtLink } from "#components";

const { data } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, required: true }
});

</script>
