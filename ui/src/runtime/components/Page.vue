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
import { useContext, useEvents, useFormat, useHead, usePages } from "#imports";
import { Bake } from "#components";

const context = useContext();
const events = useEvents();
const { asClasses } = useFormat();
const pages = usePages();
const route = useRoute();
const { public: { components } } = useRuntimeConfig();

useHead({ title: components?.Page?.title });

context.provideEvents(events);
context.providePageContext(reactive({}));

const name = route.matched[0].name;
const className = name.replace("[", "").replace("]", "");

const descriptor = await pages.fetch(name);
const classes = [asClasses("page"), asClasses(className, "b-route--")];
</script>
