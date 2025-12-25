<template>
  <Bake
    v-for="(part, i) in parts"
    :key="`part-${i}`"
    :name="`parts/${i}`"
    :descriptor="part"
  />
</template>
<script setup>
import { Bake } from "#components";

const { schema } = defineProps({
  schema: { type: Array, default: () => [] }
});

const { parts } = schema;
</script>
