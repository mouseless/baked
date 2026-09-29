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
    descriptor: giveMe.anInputCheckbox({
      label: "Spec: Label"
    }),
    model: ref(true)
  },
  {
    name: "Indeterminate",
    descriptor: giveMe.anInputCheckbox({
      label: "Spec: Indeterminate",
      indeterminate: true
    }),
    model: ref(undefined)
  }
];
</script>
