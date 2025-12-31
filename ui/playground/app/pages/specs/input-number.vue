<template>
  <UiSpec
    title="Input Number"
    :variants
    use-model
  />
</template>
<script setup>
import { ref } from "vue";
import giveMe from "@utils/giveMe";

const variants = [
  {
    name: "Base",
    descriptor: giveMe.anInputNumber({
      label: "Spec: Label"
    }),
    model: ref()
  },
  {
    name: "Long Label",
    descriptor: giveMe.anInputNumber({
      label: "Spec: This is a very very very very long label"
    }),
    model: ref()
  }
];
</script>
