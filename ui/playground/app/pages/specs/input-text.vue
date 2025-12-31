<template>
  <UiSpec
    title="Input Text"
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
    descriptor: giveMe.anInputText({
      label: "Spec: Label"
    }),
    model: ref()
  },
  {
    name: "Long Label",
    descriptor: giveMe.anInputText({
      label: "Spec: This is a very very very very long label"
    }),
    model: ref()
  }
];
</script>
