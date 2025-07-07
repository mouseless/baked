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
      title: "PageTitle.Title",
      description: "PageTitle.Description"
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
  },
  {
    name: "Long Description",
    descriptor: giveMe.aPageTitle({
      description: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum."
    })
  }
];
</script>
