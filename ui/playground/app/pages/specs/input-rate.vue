<template>
  <UiSpec
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
    descriptor: giveMe.anInputRate({
      label: "Spec: Label"
    }),
    model: ref()
  }
];
</script>