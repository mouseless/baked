<template>
  <UiSpec
    title="Menu Page"
    :variants="variants"
    :no-loading-variant="true"
  />
</template>
<script setup>
import { reactive } from "vue";
import giveMe from "~/utils/giveMe";
import { useContext } from "#imports";

const context = useContext();
context.setPage(reactive({}));

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
          filterableLinks: [{ link: giveMe.anExpected({ testId: "LINK_1", value: "VALUE_1"}) }]
        },
        {
          title: "Section 2",
          filterableLinks: [{ link: giveMe.anExpected({ testId: "LINK_2", value: "VALUE_2"}) }]
        }
      ]
    })
  },
  {
    name: "Filter Links",
    descriptor: giveMe.aMenuPage({
      pageContextKey: "key",
      header: giveMe.aFilter({contextKey: "key"}),
      sections: [
        {
          title: "Section 1",
          filterableLinks: [{ title: "A_VALUE", link: giveMe.anExpected({ testId: "LINK_1", value: "A_VALUE"}) }]
        },
        {
          title: "Section 2",
          filterableLinks: [{ title: "B_VALUE", link: giveMe.anExpected({ testId: "LINK_2", value: "B_VALUE"}) }]
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
