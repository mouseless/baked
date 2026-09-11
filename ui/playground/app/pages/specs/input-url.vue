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
    descriptor: giveMe.anInputUrl({
      label: "Spec: Label"
    }),
    model: ref("https://baked.mouseless.codes")
  }
];
</script>