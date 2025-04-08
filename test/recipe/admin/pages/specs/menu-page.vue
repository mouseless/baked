<template>
  <UiSpec
    title="Menu Page"
    :variants="variants"
    :no-loading-variant="true"
  />
</template>
<script setup>
import giveMe from "~/utils/giveMe";

const variants = [
  {
    name: "Header and Links",
    descriptor: giveMe.aMenuPage({
      header: giveMe.anExpected({ testId: "header", value: "PAGE TITLE" }),
      links: new Array(12).fill(0).map((_, i) =>
        giveMe.anExpected({ testId: `LINK_${i}`, value: `VALUE_${i}`})
      )
    })
  },
  {
    name: "Sections",
    descriptor: giveMe.aMenuPage({
      header: null,
      sections: [
        {
          title: "Section 1",
          links: [giveMe.anExpected({ testId: "LINK_1", value: "VALUE_1"})]
        },
        {
          title: "Section 2",
          links: [giveMe.anExpected({ testId: "LINK_2", value: "VALUE_2"})]
        }
      ]
    })
  },
  {
    name: "No Header",
    descriptor: giveMe.aMenuPage({
      header: null,
      links: [
        giveMe.anExpected({ value: "Menu Placeholder 1"}),
        giveMe.anExpected({ value: "Menu Placeholder 2"})
      ]
    })
  }
];
</script>
