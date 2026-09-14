<template>
  <UiSpec :variants />
</template>
<script setup>
import giveMe from "@utils/giveMe";

const variants = [
  {
    name: "Base",
    descriptor: giveMe.aTextLink({
      value: "https://baked.mouseless.codes"
    })
  },
  {
    name: "Truncated",
    descriptor: giveMe.aTextLink({
      value: "https://baked.mouseless.codes/very/long/path/for/demo/purpose",
      maxLength: 24
    })
  }
];
</script>
