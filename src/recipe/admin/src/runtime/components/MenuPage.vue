<template>
  <div class="space-y-4">
    <Bake
      v-if="header"
      name="header"
      :descriptor="header"
    />
    <template v-if="sections.default">
      <div class="grid gap-4 grid-cols-1 md:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 2xl:grid-cols-6">
        <Bake
          v-for="(link, i) in sections.default.links"
          :key="link.schema.route"
          :name="`links/${i}`"
          :descriptor="link"
        />
      </div>
    </template>
    <template v-else>
      <div class="flex flex-col gap-4">
        <div
          v-for="sectionId in Object.keys(sections)"
          :key="sectionId"
          :data-testid="sectionId"
        >
          <h2 :data-testid="`${sectionId}_NAME`" class="text-lg font-semibold">
            {{ sections[sectionId].name }}
          </h2>
          <div class="grid gap-4 grid-cols-1 md:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 2xl:grid-cols-6">
            <Bake
              v-for="(link, i) in sections[sectionId].links"
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

const { header, links } = schema;

const sections = links.reduce((acc, link) => {
  const section = link.section ?? { id: "default", name: "Default" };
  if(!acc[section.id]) {
    acc[section.id] = { name: section.name, links: [] };
  }

  acc[section.id].links.push(link);
  return acc;
}, {});
</script>
