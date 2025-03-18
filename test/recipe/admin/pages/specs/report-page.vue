<template>
  <UiSpec
    title="Report Page"
    :variants="variants"
    :no-loading-variant="true"
  />
</template>
<script setup>
import giveMe from "~/utils/giveMe";

const variants = [
  {
    name: "Base",
    descriptor: giveMe.aReportPage({
      header: giveMe.anExpected({ testId: "header", value: "PAGE TITLE" })
    })
  }
];
</script>
