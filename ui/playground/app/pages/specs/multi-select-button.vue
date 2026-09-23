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
    descriptor: giveMe.aMultiSelectButton({
      label: "Label",
      data: ["OPTION_1", "OPTION_2"]
    }),
    model: ref()
  }
];
</script>
