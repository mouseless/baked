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
      sections: [
        giveMe.aMenuPageSection({ links: new Array(12).fill(0).map((_, i) =>
          giveMe.aFilterable({
            component: giveMe.anExpected({ testId: `LINK_${i}`, value: `VALUE_${i}`})
          })
        )})
      ]
    })
  },
  {
    name: "Sections",
    descriptor: giveMe.aMenuPage({
      header: null,
      sections: [
        giveMe.aMenuPageSection({ title: "Section 1" }),
        giveMe.aMenuPageSection({ title: "Section 2" })
      ]
    })
  },
  {
    name: "Filter Links",
    descriptor: giveMe.aMenuPage({
      filterPageContextKey: "key",
      header: giveMe.aFilter({pageContextKey: "key"}),
      sections: [
        giveMe.aMenuPageSection({
          title: "Section 1",
          links: [
            giveMe.aFilterable({ title: "A_VALUE", component: giveMe.anExpected({ testId: "LINK_1", value: "A_VALUE" })})
          ]
        }),
        giveMe.aMenuPageSection({
          title: "Section 2",
          links: [
            giveMe.aFilterable({ title: "B_VALUE", component: giveMe.anExpected({ testId: "LINK_2", value: "B_VALUE"})})
          ]
        })
      ]
    })
  },
  {
    name: "No Header",
    descriptor: giveMe.aMenuPage({
      header: null,
      sections: [
        giveMe.aMenuPageSection({
          links: [
            giveMe.aFilterable({
              component: giveMe.anExpected({ value: "Menu Placeholder 1"})
            }),
            giveMe.aFilterable({
              component: giveMe.anExpected({ value: "Menu Placeholder 2"})
            })
          ]
        })
      ]
    })
  }
];
</script>
