<template>
  <Bake
    v-for="(part, i) in parts"
    v-bind="$attrs"
    :key="`part-${i}`"
    :name="`parts/${i}`"
    :descriptor="part"
  />
</template>
<script setup>
import { Bake } from "#components";

const { schema } = defineProps({
  schema: { type: Object, required: true }
});

const { parts } = schema;
</script>
