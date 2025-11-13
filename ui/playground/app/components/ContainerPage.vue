<template>
  <div class="flex gap-4 [&>*]:w-full">
    <Bake
      v-for="(content, i) in contents"
      :key="`contents/${i}`"
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

const { contents } = schema;
</script>