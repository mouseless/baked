<template>
  <UiSpec
    title="Search Box"
    :variants="variants"
    :use-model="true"
    variant-class="inline"
  />
</template>
<script setup>
import { ref } from "vue";
import giveMe from "~/utils/giveMe";

const variants = [
  {
    name: "Search",
    descriptor: giveMe.aSearchBox({
      placeholder: "empty"
    }),
    model: ref()
  }
];
</script>
