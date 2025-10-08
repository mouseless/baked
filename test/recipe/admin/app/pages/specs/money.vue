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
    descriptor: giveMe.aContainer({
      contents: [giveMe.aMoney({ data: 1_499 }), giveMe.aMoney({ data: -1_499 })]
    })
  },
  {
    name: "Decimal Digits",
    descriptor: giveMe.aContainer({
      contents: [giveMe.aMoney({ data: 999.99 }), giveMe.aMoney({ data: -999.99 })]
    })
  },
  {
    name: "Fractionless Trailing Zeros",
    descriptor: giveMe.aContainer({
      contents: [giveMe.aMoney({ data: 200.00 }), giveMe.aMoney({ data: -200.00 })]
    })
  },
  {
    name: "Millions",
    descriptor: giveMe.aContainer({
      contents: [giveMe.aMoney({ data: 1_499_000 }), giveMe.aMoney({ data: -1_499_000 })]
    })
  },
  {
    name: "Billions",
    descriptor: giveMe.aContainer({
      contents:[giveMe.aMoney({ data: 1_499_000_000 }), giveMe.aMoney({ data: -1_499_000_000 })]
    })
  }
];
</script>
