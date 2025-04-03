<template>
  <Bake
    name="page"
    :descriptor="descriptor"
  />
</template>
<script setup>
import { reactive } from "vue";
import { useRoute, useRuntimeConfig } from "#app";
import { useContext, useHead, usePages } from "#imports";
import { Bake } from "#components";

const context = useContext();
const pages = usePages();
const route = useRoute();
const { public: { components } } = useRuntimeConfig();

useHead({ title: components?.Page?.title });

context.setPage(reactive({}));
const name = route.params.baked[0] ?? "index";
const descriptor = await pages.fetch(name);
</script>
