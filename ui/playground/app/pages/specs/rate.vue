<template>
  <UiSpec :variants />
</template>
<script setup>
import giveMe from "@utils/giveMe";

const variants = [
  {
    name: "Base",
    descriptor: giveMe.aRate({ data: 0.5 })
  },
  {
    name: "Decimal Point",
    descriptor: giveMe.aRate({ data: 0.499 })
  }
];
</script>