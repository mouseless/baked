<template>
  <Bake
    name="page"
    :descriptor="descriptor"
  />
</template>
<script setup>
import { useRoute, useRuntimeConfig } from "#app";
import { useHead, usePages } from "#imports";
import Bake from "./Bake.vue";

const route = useRoute();
const pages = usePages();
const { public: { components } } = useRuntimeConfig();
useHead({ title: components?.Page?.title });

const name = route.params.baked[0] ?? "index";
const descriptor = await pages.fetch(name);
</script>
