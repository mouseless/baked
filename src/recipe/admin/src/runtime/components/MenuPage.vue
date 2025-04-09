<template>
  <div class="space-y-4">
    <Bake
      v-if="header"
      name="header"
      :descriptor="header"
    />
    <div
      v-if="sectionsData.length > 0"
      class="flex flex-col gap-6"
    >
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
            v-for="(link, i) in section.links"
            :key="link.component.schema.route"
            :name="`links/${i}`"
            :descriptor="link.component"
          />
        </div>
      </div>
    </div>
    <div v-if="sectionsData.length === 0">
      {{ components?.MenuPage?.noFoundMessage || "No item available!" }}
    </div>
  </div>
</template>
<script setup>
import { useRuntimeConfig } from "#app";
import { Bake } from "#components";
import { useContext } from "#imports";
import { Divider } from "primevue";
import { ref, watch } from "vue";

const context = useContext();
const { public: { components, composables } } = useRuntimeConfig();

const { schema } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, default: null }
});

const { header, sections, pageContextKey } = schema;
const locale = composables?.useFormat?.locale || "en-US";
const sectionsData = ref(sections);

const page = context.page();
// Listen in context if any filter is applied
if(pageContextKey) {
  watch(() => page[pageContextKey], (newValue, _) => {
    // Apply filter to links
    const sectionsWithFilteredLinks = sections.map(section => ({
      title: section.title,
      links: section.links.filter(link =>
        link.title?.toLocaleLowerCase(locale).startsWith(newValue.toLocaleLowerCase(locale)))
    }));

    // If there are no links left in the sections after filter, filter the section too
    sectionsData.value = sectionsWithFilteredLinks.filter(section => section.links.length > 0);
  });
}
</script>
