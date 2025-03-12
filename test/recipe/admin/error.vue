<template>
  <Bake :descriptor="descriptor"/>
</template>
<script setup>
import { Bake } from "#components";

const descriptor = {
  type: "Error"
};
</script>