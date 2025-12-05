<template>
  <div class="flex gap-4">
    <Bake
      v-for="(content, i) in contents"
      :key="`contents/${i}`"
      v-model="model"
      :name="`contents/${i}`"
      :descriptor="content"
    />
  </div>
</template>
<script setup>
import { Bake } from "#components";

const { schema } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, required: true }
});
const model = defineModel({ type: null, required: false });

const { contents } = schema;
</script>
