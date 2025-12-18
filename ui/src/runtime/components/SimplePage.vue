<template>
  <div class="flex flex-col gap-4">
    <Bake
      name="title"
      :descriptor="title"
    />
    <div class="pt-4 flex flex-col gap-4 items-center">
      <div
        v-for="content in contents"
        :key="`content-${content.key}`"
        class="w-full max-w-screen-xl 3xl:max-w-screen-2xl"
      >
        <Bake
          :name="`contents/${content.key}`"
          :descriptor="content.component"
        />
      </div>
    </div>
  </div>
</template>
<script setup>
import { Bake } from "#components";

const { schema } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, default: null }
});

const { title, contents } = schema;
</script>
