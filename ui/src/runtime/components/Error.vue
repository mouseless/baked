<template>
  <Bake
    v-if="error"
    name="error"
    :descriptor="error"
  />
</template>
<script setup>
import { reactive } from "vue";
import { useRuntimeConfig } from "#app";
import { useContext, useEvents } from "#imports";
import { Bake } from "#components";

const context = useContext();
const events = useEvents();
const { public: { error } } = useRuntimeConfig();

context.provideEvents(events);
context.providePageContext(reactive({}));
</script>
