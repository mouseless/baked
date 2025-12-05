<template>
  <UiSpec
    title="Data Panel"
    :variants="variants"
    :no-loading-variant="true"
  />
</template>
<script setup>
import giveMe from "@utils/giveMe";

const variants = [
  {
    name: "Base",
    descriptor: giveMe.aDataPanel({
      title: { type: "Inline", value: "Spec: Title" },
      collapsed: false,
      content: giveMe.anExpected({ testId: "content", value: "TEST DATA" })
    })
  },
  {
    name: "Base with computed title",
    descriptor: giveMe.aDataPanel({
      title: { type: "Computed", composable: "useDelayedData", options: { type: "Inline", value: { ms: 1, data: "Title" } } },
      collapsed: false
    })
  },
  {
    name: "Collapsed",
    descriptor: giveMe.aDataPanel({
      collapsed: true,
      content: giveMe.anExpected({ testId: "content", value: "DISPLAY ON EXPAND" })
    })
  },
  {
    name: "Inputs",
    descriptor: giveMe.aDataPanel({
      inputs: [
        giveMe.anInput({
          name: "required",
          required: true,
          component: giveMe.anInputText({
            testId: "required"
          })
        }),
        giveMe.anInput({
          name: "optional",
          required: false,
          component: giveMe.anInputText({
            testId: "optional"
          })
        })
      ],
      content: giveMe.anExpected({
        testId: "content",
        data: giveMe.theInjectedData()
      })
    })
  }
];
</script>
