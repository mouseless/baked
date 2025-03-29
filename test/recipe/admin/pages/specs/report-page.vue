<template>
  <UiSpec
    title="Report Page"
    :variants="variants"
    :no-loading-variant="true"
    :full-page="true"
  />
</template>
<script setup>
import giveMe from "~/utils/giveMe";

const variants = [
  {
    name: "Base",
    descriptor: giveMe.aReportPage({
      title: "TITLE",
      description: "DESCRIPTION",
      tabs: [
        giveMe.aReportPageTab({
          id: "tab 1",
          title: "TAB 1",
          icon: giveMe.anExpected({ testId: "icon 1", value: "I." }),
          contents: [
            giveMe.aReportPageTabContent({
              component: giveMe.anExpected({ testId: "content 1.1", value: "CONTENT 1.1" })
            }),
            giveMe.aReportPageTabContent({
              component: giveMe.anExpected({ testId: "content 1.2", value: "CONTENT 1.2" })
            })
          ]
        }),
        giveMe.aReportPageTab({
          id: "tab 2",
          title: "TAB 2",
          icon: giveMe.anExpected({ testId: "icon 2", value: "II." }),
          contents: [
            giveMe.aReportPageTabContent({
              component: giveMe.anExpected({ testId: "content 2.1", value: "CONTENT 2.1" })
            }),
            giveMe.aReportPageTabContent({
              component: giveMe.anExpected({ testId: "content 2.2", value: "CONTENT 2.2" })
            })
          ]
        })
      ]
    })
  },
  {
    name: "Full Page",
    descriptor: giveMe.aReportPage({
      tabs: [
        giveMe.aReportPageTab({
          contents: [
            giveMe.aReportPageTabContent({ fullScreen: true })
          ]
        })
      ]
    })
  },
  {
    name: "Narrow",
    descriptor: giveMe.aReportPage({
      tabs: [
        giveMe.aReportPageTab({
          contents: [
            giveMe.aReportPageTabContent({ narrow: true }),
            giveMe.aReportPageTabContent({ narrow: true })
          ]
        })
      ]
    })
  }
];
</script>
