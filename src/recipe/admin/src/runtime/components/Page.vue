<template>
  <Bake
    :name="name"
    :descriptor="descriptor"
    :class="classes"
  />
</template>
<script setup>
import { reactive } from "vue";
import { useRoute, useRuntimeConfig } from "#app";
import { useContext, useFormat, useHead, usePages } from "#imports";
import { Bake } from "#components";

const context = useContext();
const { asClasses } = useFormat();
const pages = usePages();
const route = useRoute();
const { public: { components } } = useRuntimeConfig();

useHead({ title: components?.Page?.title });

context.setPage(reactive({}));
const name = route.params?.baked === "" ? "index" : route.params?.baked.join("/");
const descriptor = await pages.fetch(name);
const classes = [asClasses("page"), asClasses(name, "b-route--")];
</script>
