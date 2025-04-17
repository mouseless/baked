<template>
  <div class="py-4 flex flex-col gap-4 items-center">
    <PageTitle :schema="title" />
    <Bake
      v-for="(component, i) in components"
      :key="`component/${i}`"
      :name="`component/${i}`"
      :descriptor="component"
    />
  </div>
</template>
<script setup>
import { Bake } from "#components";

const { schema } = defineProps({
  schema: { type: null, required: true }
});

const { title, components } = schema;
</script>