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
            text-xs font-bold
          "
        >
          {{ l(section.title).toLocaleUpperCase(locale) }}
        </h2>
        <Divider
          v-if="section.title"
          type="dashed"
          class="mt-2"
        />
        <div
          class="
            grid gap-4 grid-cols-1
            md:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 3xl:grid-cols-6
          "
        >
          <Bake
            v-for="(link, i) in section.links"
            :key="link.title"
            :name="`links/${i}`"
            :descriptor="link.component"
          />
        </div>
      </div>
    </div>
    <div v-if="sectionsData.length === 0">
      {{ lc("No item available!") }}
    </div>
  </div>
</template>
<script setup>
import { useRuntimeConfig } from "#app";
import { Bake } from "#components";
import { useContext, useLocalization } from "#imports";
import { Divider } from "primevue";
import { ref, watch } from "vue";

const context = useContext();
const { localize: l } = useLocalization();
const { localize: lc } = useLocalization("MenuPage");
const { public: { composables } } = useRuntimeConfig();

const { schema } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, default: null }
});

const { header, sections, filterPageContextKey } = schema;
const locale = composables?.useFormat?.locale || "en-US";
const sectionsData = ref(sections);

const page = context.page();
// listen in context if any filter is applied
if(filterPageContextKey) {
  watch(
    () => page[filterPageContextKey],
    (newValue, oldValue) => {
      if(newValue === oldValue || newValue === undefined) { return; }

      if(!newValue.trim()) {
        sectionsData.value = sections;

        return;
      }

      const searchTerm = newValue.toLocaleLowerCase(locale);
      const sectionsWithFilteredLinks = sections.map(section => ({
        title: section.title,
        links: section.links.filter(link => {
          const title = l(link.title);

          return title?.toLocaleLowerCase(locale).startsWith(searchTerm);
        })
      }));

      sectionsData.value = sectionsWithFilteredLinks.filter(section => section.links.length > 0);
    },
    { immediate: true }
  );
}
</script>
