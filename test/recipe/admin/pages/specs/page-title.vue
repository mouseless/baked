<template>
  <UiSpec
    title="Page Title"
    :variants="variants"
    :no-loading-variant="true"
  />
</template>
<script setup>
import giveMe from "~/utils/giveMe";

const variants = [
  {
    name: "Base",
    descriptor: giveMe.aPageTitle({
      title: "Spec.Title",
      description: "Spec.Description"
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
      title: "Spec.PAGE_TITLE",
      description: null
    })
  },
  {
    name: "Long Description",
    descriptor: giveMe.aPageTitle({
      description: "Spec.Lorem_ipsum"
    })
  }
];
</script>
