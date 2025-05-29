<template>
  <UiSpec
    title="Money"
    :variants="variants"
  />
</template>
<script setup>
import giveMe from "~/utils/giveMe";

const variants = [
  {
    name: "Base",
    descriptor: giveMe.aMoney({ data: 1_499 })
  },
  {
    name: "Decimal Digits",
    descriptor: giveMe.aMoney({ data: 999.99 })
  },
  {
    name: "Millions",
    descriptor: giveMe.aMoney({ data: 1_499_000 })
  },
  {
    name: "Billions",
    descriptor: giveMe.aMoney({ data: 1_499_000_000 })
  }
];
</script>
