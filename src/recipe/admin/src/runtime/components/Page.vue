<template>
  <Bake
    v-if="pageDescriptor"
    name="page"
    :descriptor="pageDescriptor"
  />
</template>
<script setup>
import { computed, onMounted, ref } from "vue";
import { setPageLayout, useRuntimeConfig } from "#app";
import { useHead, usePages } from "#imports";
import Bake from "./Bake.vue";

const { routeParams } = defineProps({
  routeParams: {
    type: null,
    required: true
  }
});

const pages = usePages();
const { public: { components } } = useRuntimeConfig();
useHead({ title: components?.Page?.title });

const pageDescriptor = ref();
const pageName = computed(() => routeParams[0] ?? "index");

onMounted(async() => {
  const descriptor = await pages.fetch(pageName.value);

  if(descriptor.schema?.layout) {
    setPageLayout(descriptor.schema.layout);
  }

  pageDescriptor.value = descriptor;
});
</script>
