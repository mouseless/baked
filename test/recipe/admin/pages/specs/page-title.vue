<template>
  <ComponentSpec
    title="Page Title"
    :variants="variants"
  />
</template>
<script setup>
import giveMe from "~/utils/giveMe";

const variants = [
  {
    name: "Base",
    descriptor: giveMe.aPageTitle({
      title: "PAGE TITLE",
      description: "PAGE DESCRIPTION"
    })
  },
  {
    name: "Actions",
    descriptor: giveMe.aPageTitle({
      actions: [
        giveMe.anExpected({ testId: "ACTION_1", value: "VALUE_1"}),
        giveMe.anExpected({ testId: "ACTION_2", value: "VALUE_2"})
      ]
    })
  },
  {
    name: "No Description",
    descriptor: giveMe.aPageTitle({
      title: "PAGE TITLE",
      description: null
    })
  }
];
</script>
