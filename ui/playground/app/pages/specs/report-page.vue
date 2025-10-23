<template>
  <UiSpec
    title="Report Page"
    :variants="variants"
    :no-loading-variant="true"
    :full-page="true"
  />
</template>
<script setup>
import giveMe from "@utils/giveMe";

const variants = [
  {
    name: "Base",
    descriptor: giveMe.aReportPage({
      title: "Spec: Title",
      description: "Spec: Description",
      tabs: [
        giveMe.aReportPageTab({
          id: "tab 1",
          title: "Spec: Tab 1",
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
          title: "Spec: Tab 2",
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
    name: "Show When",
    descriptor: giveMe.aReportPage({
      queryParameters: [
        giveMe.aParameter({
          component: giveMe.aSelectButton({
            data: ["SHOW"],
            selectionPageContextKey: "selection-is",
            allowEmpty: true
          })
        })
      ],
      tabs: [
        giveMe.aReportPageTab({
          id: "tab-1",
          title: "Spec: Tab 1",
          contents: [
            giveMe.aReportPageTabContent(),
            giveMe.aReportPageTabContent({
              component: giveMe.anExpected({ testId: "content-1", value: "CONTENT 1" }),
              showWhen: "selection-is:SHOW"
            })
          ]
        }),
        giveMe.aReportPageTab({
          id: "tab-2",
          title: "Spec: Tab 2",
          showWhen: "selection-is:SHOW",
          contents: [
            giveMe.aReportPageTabContent({
              component: giveMe.anExpected({ testId: "content-2", value: "CONTENT 2" }),
              showWhen: "selection-is:SHOW"
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
          component: giveMe.anInputText({
            testId: "required"
          })
        }),
        giveMe.aParameter({
          name: "optional",
          required: false,
          component: giveMe.anInputText({
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
