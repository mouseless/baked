<template>
  <UiSpec
    title="API Panel"
    :variants="variants"
  />
</template>
<script setup>
import giveMe from "~/utils/giveMe";

const variants = [
  {
    name: "Base",
    descriptor: giveMe.anApiPanel({
      title: "PANEL TITLE",
      collapsed: true,
      viewer: giveMe.anExpected({ testId: "viewer", value: "TEST DATA" })
    })
  }
];
</script>
