<template>
  <UiSpec
    :variants
    use-model
  />
</template>
<script setup>
import { computed, ref } from "vue";
import giveMe from "@utils/giveMe";

const variants = [
  {
    name: "Base",
    descriptor: giveMe.anInputUrl({
      label: "Spec: Label"
    }),
    model: ref("https://baked.mouseless.codes")
  },
  {
    name: "SetModel",
    descriptor: giveMe.anInputUrl({
      label: "Spec: Label"
    }),
    model: ref(),
    validation: computed(() => ({
      severity: "error"
    }))
  }
];
</script>