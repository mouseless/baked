<template>
  <UiSpec
    title="Simple Page"
    :variants
    no-loading-variant
    full-page
  />
</template>
<script setup>
import giveMe from "@utils/giveMe";

const variants = [
  {
    name: "Base",
    descriptor: giveMe.aSimplePage({
      title: giveMe.anExpected({ testId: "title", value: "TITLE" }),
      contents: [
        giveMe.anExpected({ testId: "content-1", value: "CONTENT-1" }),
        giveMe.anExpected({ testId: "content-2", value: "CONTENT-2" })
      ]
    })
  }
];
</script>
