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
    descriptor: giveMe.anInputMailAddress({
      label: "Spec: Label"
    }),
    model: ref("user@example.com")
  },
  {
    name: "SetModel",
    descriptor: giveMe.anInputMailAddress({
      label: "Spec: Label"
    }),
    model: ref(),
    validation: computed(() => ({
      severity: "error"
    }))
  },
  {
    name: "InputValidation",
    descriptor: giveMe.anInputMailAddress({
      label: "Spec: Label"
    }),
    model: ref(),
    validation: computed(() => ({
      severity: "error"
    }))
  }
];
</script>