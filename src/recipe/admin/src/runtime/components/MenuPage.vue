<template>
  <div class="space-y-4">
    <Bake
      v-if="header"
      name="header"
      :descriptor="header"
    />
    <div class="grid gap-4 grid-cols-1 md:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 2xl:grid-cols-6">
      <Bake
        v-for="(link, i) in links"
        :key="link.schema.route"
        :name="`links/${i}`"
        :descriptor="link"
      />
    </div>
  </div>
</template>
<script setup>
import { Bake } from "#components";

const { schema } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, default: null }
});

const { header, links } = schema;
</script>
