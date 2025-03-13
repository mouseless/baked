<template>
  <UiSpec
    title="Data Panel"
    :variants="variants"
    :no-loading-variant="true"
  />
</template>
<script setup>
import giveMe from "~/utils/giveMe";

const variants = [
  {
    name: "Base",
    descriptor: giveMe.aDataPanel({
      title: "TITLE",
      collapsed: false,
      content: giveMe.anExpected({ testId: "content", value: "TEST DATA" })
    })
  },
  {
    name: "Collapsed",
    descriptor: giveMe.aDataPanel({
      collapsed: true,
      content: giveMe.anExpected({ testId: "content", value: "DISPLAY ON EXPAND" })
    })
  }
];
</script>
