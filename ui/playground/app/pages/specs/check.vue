<template>
  <UiSpec :variants />
</template>
<script setup>
import giveMe from "@utils/giveMe";

const variants = [
  {
    name: "True",
    descriptor: giveMe.aCheck({ data: true })
  },
  {
    name: "False",
    descriptor: giveMe.aCheck({ data: false })
  },
  {
    name: "Null",
    descriptor: giveMe.aCheck({ data: null })
  }
];
</script>