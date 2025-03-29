<template>
  <Bake
    v-if="pageDescriptor"
    name="page"
    :descriptor="pageDescriptor"
  />
</template>
<script setup>
import { computed, onMounted, ref } from "vue";
import { useRuntimeConfig } from "#app";
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

onMounted(async() => pageDescriptor.value = await pages.fetch(pageName.value));
</script>
