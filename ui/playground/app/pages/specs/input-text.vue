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
    descriptor: giveMe.anInputText({
      label: "Spec: Label"
    }),
    model: ref("initial value")
  },
  {
    name: "Long Label",
    descriptor: giveMe.anInputText({
      label: "Spec: This is a very very very very long label"
    }),
    model: ref()
  },
  {
    name: "Target Prop",
    descriptor: giveMe.anInputText({
      label: "Id",
      targetProp: "id"
    }),
    model: ref()
  }
];
</script>