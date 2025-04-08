<template>
  <div class="space-y-4">
    <Bake
      v-if="header"
      name="header"
      :descriptor="header"
    />
    <div class="flex flex-col gap-6">
      <div
        v-for="section in sections"
        :key="section.title"
      >
        <h2
          v-if="section.title"
          class="
            text-zinc-400 dark:text-zinc-600
            uppercase text-xs font-bold
          "
        >
          {{ section.title }}
        </h2>
        <Divider
          v-if="section.title"
          type="dashed"
          class="mt-2"
        />
        <div class="grid gap-4 grid-cols-1 md:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 2xl:grid-cols-6">
          <Bake
            v-for="(link, i) in section.links"
            :key="link.schema.route"
            :name="`links/${i}`"
            :descriptor="link"
          />
        </div>
      </div>
    </div>
  </div>
</template>
<script setup>
import { Divider } from "primevue";
import { Bake } from "#components";

const { schema } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, default: null }
});

const { header, sections } = schema;
</script>
