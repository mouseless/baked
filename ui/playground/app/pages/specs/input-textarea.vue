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
    descriptor: giveMe.anInputTextarea({
      label: "Spec: Label"
    }),
    model: ref("initial value")
  }
];
</script>
