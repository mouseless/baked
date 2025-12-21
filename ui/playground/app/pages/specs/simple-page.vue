<template>
  <UiSpec
    title="Simple Page"
    :variants
    no-loading-variant
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
        giveMe.aContent({ component: giveMe.anExpected({ testId: "content-1", value: "CONTENT_1" }) }),
        giveMe.aContent({ component: giveMe.anExpected({ testId: "content-2", value: "CONTENT_2" }) })
      ]
    })
  }
];
</script>
