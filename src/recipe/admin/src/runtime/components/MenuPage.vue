<template>
  <div class="space-y-4">
    <Bake
      v-if="header"
      name="header"
      :descriptor="header"
    />
    <template v-if="!sections">
      <div class="grid gap-4 grid-cols-1 md:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 2xl:grid-cols-6">
        <Bake
          v-for="(link, i) in links"
          :key="link.schema.route"
          :name="`links/${i}`"
          :descriptor="link"
        />
      </div>
    </template>
    <template v-else>
      <div class="flex flex-col gap-4">
        <div
          v-for="section in sections"
          :key="section.id"
          :data-testid="section.id"
        >
          <h2
            :data-testid="`${section.id}_NAME`"
            class="text-lg text-gray-400"
          >
            {{ section.name }}
          </h2>
          <div class="grid gap-4 mt-2 grid-cols-1 md:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 2xl:grid-cols-6">
            <Bake
              v-for="(link, i) in section.links"
              :key="link.schema.route"
              :name="`links/${i}`"
              :descriptor="link"
            />
          </div>
        </div>
      </div>
    </template>
  </div>
</template>
<script setup>
import Bake from "./Bake.vue";

const { schema } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, default: null }
});

const { header, links, sections } = schema;
</script>
