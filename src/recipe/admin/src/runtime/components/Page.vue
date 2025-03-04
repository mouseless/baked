<template>
  <Bake
    v-if="pageDescriptor"
    :descriptor="pageDescriptor"
  />
</template>
<script setup>
import { computed, onMounted, provide, ref } from "vue";
import { useHead, usePages, useRuntimeConfig }  from "#app";
import Bake from "./Bake.vue";

const { routeParams } = defineProps({
  routeParams: {
    type: null,
    required: true
  }
});

const pages = usePages();
const { public: { title } } = useRuntimeConfig();
useHead({ title });

const pageDescriptor = ref();
const pageName = computed(() => routeParams[0] ?? "index");

provide("routeParams", routeParams);

onMounted(async() => pageDescriptor.value = await pages.fetch(pageName.value));
</script>
