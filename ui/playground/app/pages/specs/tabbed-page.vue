<template>
  <UiSpec
    title="Tabbed Page"
    :variants
    no-loading-variant
  />
</template>
<script setup>
import giveMe from "@utils/giveMe";

const variants = [
  {
    name: "Base",
    descriptor: giveMe.aTabbedPage({
      title: "Spec: Title",
      description: "Spec: Description",
      tabs: [
        giveMe.aTab({
          id: "tab 1",
          title: "Spec: Tab 1",
          icon: giveMe.anExpected({ testId: "icon 1", value: "I." }),
          contents: [
            giveMe.aContent({
              component: giveMe.anExpected({ testId: "content 1.1", value: "CONTENT 1.1" })
            }),
            giveMe.aContent({
              component: giveMe.anExpected({ testId: "content 1.2", value: "CONTENT 1.2" })
            })
          ]
        }),
        giveMe.aTab({
          id: "tab 2",
          title: "Spec: Tab 2",
          icon: giveMe.anExpected({ testId: "icon 2", value: "II." }),
          contents: [
            giveMe.aContent({
              component: giveMe.anExpected({ testId: "content 2.1", value: "CONTENT 2.1" })
            }),
            giveMe.aContent({
              component: giveMe.anExpected({ testId: "content 2.2", value: "CONTENT 2.2" })
            })
          ]
        })
      ]
    })
  },
  {
    name: "Show When",
    descriptor: giveMe.aTabbedPage({
      inputs: [
        giveMe.anInput({
          component: giveMe.aSelectButton({
            data: ["SHOW"],
            allowEmpty: true,
            action: giveMe.aPublishAction({ pageContextKey: "selection" })
          })
        })
      ],
      tabs: [
        giveMe.aTab({
          id: "tab-1",
          title: "Spec: Tab 1",
          contents: [
            giveMe.aContent(),
            giveMe.aContent({
              key: "content-1",
              component: giveMe.anExpected({
                testId: "content-1",
                value: "CONTENT 1" ,
                reactions: {
                  show: giveMe.aTrigger({ when: "selection", constraint: giveMe.aConstraint({ is: "SHOW" }) })
                }
              })
            })
          ]
        }),
        giveMe.aTab({
          id: "tab-2",
          title: "Spec: Tab 2",
          reactions: {
            show: giveMe.aTrigger({ when: "selection", constraint: giveMe.aConstraint({ is: "SHOW" }) })
          },
          contents: [
            giveMe.aContent({
              key: "content-2",
              component: giveMe.anExpected({ testId: "content-2", value: "CONTENT 2" })
            })
          ]
        })
      ]
    })
  },
  {
    name: "Single Tab",
    descriptor: giveMe.aTabbedPage({
      tabs: [
        giveMe.aTab({
          id: "hidden tab",
          contents: [
            giveMe.aContent({
              component: giveMe.anExpected({ testId: "content" })
            })
          ]
        })
      ]
    })
  },
  {
    name: "Inputs",
    descriptor: giveMe.aTabbedPage({
      inputs: [
        giveMe.anInput({
          name: "required",
          required: true,
          queryBound: true,
          component: giveMe.anInputText({
            testId: "required"
          })
        }),
        giveMe.anInput({
          name: "optional",
          required: false,
          queryBound: true,
          component: giveMe.anInputText({
            testId: "optional"
          })
        })
      ],
      tabs: [
        giveMe.aTab({
          contents: [
            giveMe.aContent({
              component: giveMe.anExpected({
                testId: "static-content",
                value: "HIDDEN WHEN REQUIRED IS MISSING"
              })
            }),
            giveMe.aContent({
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
