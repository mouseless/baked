<template>
  <PageTitle
    v-if="schema.title"
    :schema="schema"
  />
  <div class="grid gap-4 grid-cols-1 md:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 2xl:grid-cols-6">
    <Bake
      v-for="link in schema.links"
      :key="link.schema.route"
      :descriptor="link"
    />
  </div>
</template>
<script setup>
import Bake from "./Bake.vue";
import PageTitle from "./PageTitle.vue";

defineProps({
  schema: { type: null, required: true }
});
</script>
