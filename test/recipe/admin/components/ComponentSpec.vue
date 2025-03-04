<template>
  <PageTitle
    v-if="loaded"
    :schema="{ title, description }"
  />
  <div class="flex justify-center w-full">
    <div
       class="max-w-screen-xl flex gap-4 align-top w-4/5"
       :class="{
         'flex-col': !vertical,
         'items-center': !vertical,
         'items-start': vertical
       }"
    >
      <div
        v-for="variant in variants"
        :key="variant.name"
        :class="{
          'w-full': !vertical,
          'text-center': vertical
        }"
      >
        <h2
          :id="variant.name"
          class="font-semibold"
          :class="{
            'text-lg': !vertical,
            'mt-2': !vertical,
            '-mb-2': !vertical
          }"
        >{{variant.name}}</h2>
        <Divider v-if="!vertical" />
        <div :data-testid="variant.name">
          <Bake :descriptor="variant.descriptor" />
        </div>
      </div>
    </div>
  </div>
</template>
<script setup>
import { onMounted, ref } from "vue";
import { usePages } from "#imports";
import { Divider } from "primevue";

const { title } = defineProps({
  title: { type: String, required: true },
  variants: { type: Array, required: true },
  vertical: { type: Boolean, default: false }
});

const pages = usePages();
const description = ref();
const loaded = ref(false);

onMounted(async() => {
  const specs = await pages.fetch("specs");

  const linksWithTitle = specs.schema.links.filter(l => l.schema.title === title);
  if(linksWithTitle.length > 0) {
    description.value = linksWithTitle[0].schema.description;
  }

  loaded.value = true;
});
</script>
