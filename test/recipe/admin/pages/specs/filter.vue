<template>
  <UiSpec
    title="Filter"
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
    name: "Filter",
    descriptor: giveMe.aFilter({
      placeholder: "empty"
    }),
    model: ref()
  }
];
</script>
