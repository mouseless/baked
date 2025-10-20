<template>
  <UiSpec
    title="Text"
    :variants="variants"
    :vertical="false"
  />
</template>
<script setup>
import giveMe from "@utils/giveMe";

const variants = [
  {
    name: "Base",
    descriptor: giveMe.aText({ value: "This is a text" })
  },
  {
    name: "Max Length",
    descriptor: giveMe.aText({
      value: "This text should be truncated with elipsis when exceeds max length",
      maxLength: "This text should".length
    })
  },
  {
    name: "No Data",
    descriptor: giveMe.aText({ data: null })
  }
];
</script>
