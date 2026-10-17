<template>
  <Bake
    v-if="error"
    name="error"
    :descriptor="error"
  />
</template>
<script setup>
import { useRuntimeConfig } from "#app";
import { useContext } from "#imports";
import { Bake } from "#components";

const context = useContext();
const { public: { error } } = useRuntimeConfig();

context.providePageScope();
</script>
