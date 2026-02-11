<template>
  <UiSpec
    title="Menu Page"
    :variants="variants"
    :no-loading-variant="true"
  />
</template>
<script setup>
import giveMe from "@utils/giveMe";

const variants = [
  {
    name: "Header and Links",
    descriptor: giveMe.aMenuPage({
      header: giveMe.anExpected({ testId: "header", value: "PAGE TITLE" }),
      sections: [
        giveMe.aMenuPageSection({ links: new Array(12).fill(0).map((_, i) =>
          giveMe.aFilterable({
            component: giveMe.anExpected({ testId: `LINK_${i}`, value: `VALUE_${i}` })
          })
        ) })
      ]
    })
  },
  {
    name: "Sections",
    descriptor: giveMe.aMenuPage({
      header: null,
      sections: [
        giveMe.aMenuPageSection({ title: "Spec: Section 1" }),
        giveMe.aMenuPageSection({ title: "Spec: Section 2" })
      ]
    })
  },
  {
    name: "Filter Links",
    descriptor: giveMe.aMenuPage({
      filterEvent: "filter",
      header: giveMe.aFilter({
        action: giveMe.aPublishAction({ event: "filter" })
      }),
      sections: [
        giveMe.aMenuPageSection({
          title: "Spec: Section 1",
          links: [
            giveMe.aFilterable({ title: "Spec: A link", component: giveMe.anExpected({ testId: "LINK_1", value: "A_VALUE" }) })
          ]
        }),
        giveMe.aMenuPageSection({
          title: "Spec: Section 2",
          links: [
            giveMe.aFilterable({ title: "Spec: B link", component: giveMe.anExpected({ testId: "LINK_2", value: "B_VALUE" }) })
          ]
        })
      ]
    })
  },
  {
    name: "White Space Open Filter",
    descriptor: giveMe.aMenuPage({
      filterEvent: "filter-white-space-open",
      header: giveMe.aFilter({
        action: giveMe.aPublishAction({ event: "filter-white-space-open" }),
        ignoreWhiteSpace: true
      }),
      sections: [
        giveMe.aMenuPageSection({
          title: "Spec: Section 1",
          links: [
            giveMe.aFilterable({ title: "Spec: A link", component: giveMe.anExpected({ testId: "LINK_1", value: "A_VALUE" }) })
          ]
        })
      ]
    })
  },
  {
    name: "White Space Close Filter",
    descriptor: giveMe.aMenuPage({
      filterEvent: "filter-white-space-close",
      header: giveMe.aFilter({
        action: giveMe.aPublishAction({ event: "filter-white-space-close" }),
        ignoreWhiteSpace: false
      }),
      sections: [
        giveMe.aMenuPageSection({
          title: "Spec: Section 1",
          links: [
            giveMe.aFilterable({ title: "Spec: A link", component: giveMe.anExpected({ testId: "LINK_1", value: "A_VALUE" }) })
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
              component: giveMe.anExpected({ value: "Menu Placeholder 1" })
            }),
            giveMe.aFilterable({
              component: giveMe.anExpected({ value: "Menu Placeholder 2" })
            })
          ]
        })
      ]
    })
  }
];
</script>
