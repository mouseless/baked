<template>
  <div class="space-y-4">
    <Bake
      v-if="header"
      name="header"
      :descriptor="header"
    />
    <div class="flex flex-col gap-6">
      <div
        v-for="section in sectionsData"
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
            v-for="(filterable, i) in section.filterableLinks"
            :key="filterable.link.schema.route"
            :name="`links/${i}`"
            :descriptor="filterable.link"
          />
        </div>
      </div>
    </div>
  </div>
</template>
<script setup>
import { ref, watch } from "vue";
import { Divider } from "primevue";
import { Bake } from "#components";
import { useContext } from "#imports";

const context = useContext();

const { schema } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, default: null }
});

const { header, sections, pageContextKey } = schema;
const sectionsData = ref(sections);

const page = context.page();
// Listen in context if any filter is applied
if(pageContextKey) {
  watch(() => page[pageContextKey], (newValue, _) => {
    // Apply filter to links
    const sectionsWithFilteredLinks = sections.map(section => ({
      title: section.title,
      filterableLinks: section.filterableLinks.filter(filterable => filterable.title.toLowerCase().startsWith(newValue.toLowerCase()))
    }));

    // If there are no links left in the sections after filter, filter the section too
    sectionsData.value = sectionsWithFilteredLinks.filter(section => section.filterableLinks.length > 0);
  });
}
</script>
