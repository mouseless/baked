<template>
  <UiSpec
    title="Data Panel"
    :variants
    no-loading-variant
  />
</template>
<script setup>
import giveMe from "@utils/giveMe";

const variants = [
  {
    name: "Base",
    descriptor: giveMe.aDataPanel({
      title: giveMe.anInlineData("Spec: Title"),
      collapsed: false,
      content: giveMe.anExpected({ testId: "content", value: "TEST DATA" })
    })
  },
  {
    name: "Base with computed title",
    descriptor: giveMe.aDataPanel({
      title: giveMe.aComputedData({ composable: "useDelayedData", options: giveMe.anInlineData({ ms: 1, data: "Title" }) }),
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
          component: giveMe.anExpectedInput({
            testId: "required"
          })
        }),
        giveMe.anInput({
          name: "optional",
          required: false,
          component: giveMe.anExpectedInput({
            testId: "optional"
          })
        })
      ],
      content: giveMe.anExpected({
        testId: "content",
        data: giveMe.theParentContext()
      })
    })
  }
];
</script>
