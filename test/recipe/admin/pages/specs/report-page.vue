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
      title: "Spec.Title",
      description: "Spec.Description",
      tabs: [
        giveMe.aReportPageTab({
          id: "tab 1",
          title: "Spec.Tab_1",
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
          title: "Spec.Tab_2",
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
    name: "Single Tab",
    descriptor: giveMe.aReportPage({
      tabs: [
        giveMe.aReportPageTab({
          id: "hidden tab",
          contents: [
            giveMe.aReportPageTabContent({
              component: giveMe.anExpected({ testId: "content" })
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
          fullScreen: true,
          overflow: true,
          contents: [
            giveMe.aReportPageTabContent()
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
  },
  {
    name: "Query Parameters",
    descriptor: giveMe.aReportPage({
      queryParameters: [
        giveMe.aParameter({
          name: "required",
          required: true,
          component: giveMe.anInput({
            testId: "required"
          })
        }),
        giveMe.aParameter({
          name: "optional",
          required: false,
          component: giveMe.anInput({
            testId: "optional"
          })
        })
      ],
      tabs: [
        giveMe.aReportPageTab({
          contents: [
            giveMe.aReportPageTabContent({
              component: giveMe.anExpected({
                testId: "static-content",
                value: "HIDDEN WHEN REQUIRED IS MISSING"
              })
            }),
            giveMe.aReportPageTabContent({
              component: giveMe.anExpected({
                testId: "query-content",
                data: giveMe.theQueryData()
              })
            })
          ]
        })
      ]
    })
  }
];
</script>
