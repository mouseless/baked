<template>
  <UiSpec
    title="Data Panel"
    :variants="variants"
    :no-loading-variant="true"
  />
</template>
<script setup>
import giveMe from "~/utils/giveMe";

const variants = [
  {
    name: "Base",
    descriptor: giveMe.aDataPanel({
      title: "TITLE",
      collapsed: false,
      content: giveMe.anExpected({ testId: "content", value: "TEST DATA" })
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
    name: "Parameters",
    descriptor: giveMe.aDataPanel({
      parameters: [
        giveMe.aParameter({
          name: "requiredWithDefault",
          required: true,
          defaultValue: "default value",
          component: giveMe.anInput({
            testId: "required-with-default"
          })
        }),
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
      content: giveMe.anExpected({
        testId: "content",
        data: giveMe.theInjectedData()
      })
    })
  }
];
</script>
