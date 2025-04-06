<template>
  <UiSpec
    title="Bake"
    :variants="variants"
  />
</template>
<script setup>
import giveMe from "~/utils/giveMe";

const variants = [
  {
    name: "Base",
    descriptor: giveMe.aString({ value: "TEST" })
  }
];
</script>
