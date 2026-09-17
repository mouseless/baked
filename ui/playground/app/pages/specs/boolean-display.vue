<template>
<UiSpec :variants />
</template>
<script setup>
import giveMe from "@utils/giveMe";

const variants = [
  {
    name: "True",
    descriptor: giveMe.aBooleanDisplay({ data: true })
  },
  {
    name: "False",
    descriptor: giveMe.aBooleanDisplay({ data: false })
  },
  {
    name: "Null",
    descriptor: giveMe.aBooleanDisplay({ data: null })
  }
];
</script>