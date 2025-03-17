<template>
  <Bake
    v-if="error"
    name="error"
    :descriptor="error"
  />
</template>
<script setup>
import { useRuntimeConfig } from "#app";
import { Bake } from "#components";

const { public: { error } } = useRuntimeConfig();
</script>