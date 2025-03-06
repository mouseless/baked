<template>
  <ComponentSpec
    title="Menu Page"
    :variants="variants"
  />
</template>
<script setup>
import giveMe from "~/utils/giveMe";

const variants = [
  {
    name: "Base",
    descriptor: giveMe.aMenuPage({
      title: "PAGE TITLE",
      description: "PAGE DESCRIPTION"
    })
  },
  {
    name: "Links",
    descriptor: giveMe.aMenuPage({
      links: new Array(12).fill(0).map((_, i) =>
        giveMe.anExpected({ testId: `LINK_${i}`, value: `VALUE_${i}`})
      )
    })
  },
  {
    name: "No Description",
    descriptor: giveMe.aMenuPage({
      title: "PAGE TITLE",
      description: null
    })
  }
];
</script>
