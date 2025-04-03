<template>
  <div class="space-y-4">
    <Bake
      v-if="header"
      name="header"
      :descriptor="header"
    />
    <template v-if="groups.default">
      <div class="grid gap-4 grid-cols-1 md:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 2xl:grid-cols-6">
        <Bake
          v-for="(link, i) in groups.default.links"
          :key="link.schema.route"
          :name="`links/${i}`"
          :descriptor="link"
        />
      </div>
    </template>
    <template v-else>
      <div class="flex flex-col gap-4">
        <div
          v-for="groupId in Object.keys(groups)"
          :key="groupId"
          :data-testid="groupId"
        >
          <h2 :data-testid="`${groupId}_NAME`" class="text-lg font-semibold">
            {{ groups[groupId].name }}
          </h2>
          <div class="grid gap-4 grid-cols-1 md:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 2xl:grid-cols-6">
            <Bake
              v-for="(link, i) in groups[groupId].links"
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

const groups = links.reduce((acc, link) => {
  const group = link.group ?? { id: "default", name: "Default" };
  if(!acc[group.id]) {
    acc[group.id] = { name: group.name, links: [] };
  }

  acc[group.id].links.push(link);
  return acc;
}, {});
</script>
