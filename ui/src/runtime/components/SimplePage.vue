<template>
  <div class="flex flex-col gap-8">
    <Bake
      name="title"
      :descriptor="title"
    />
    <div class="flex justify-center">
      <Contents :contents />
    </div>
  </div>
</template>
<script setup>
import { Contents } from "#components";

const { schema } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, default: null }
});

const { title, contents } = schema;
</script>
