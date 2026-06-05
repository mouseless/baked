<template>
  <UiSpec
    :variants
    no-loading-variant
  />
</template>
<script setup>
import giveMe from "@utils/giveMe";

const variants = [
  {
    name: "Base",
    descriptor: giveMe.aDataContainer({
      content: giveMe.anExpected({ testId: "content", value: "TEST DATA" })
    })
  },
  {
    name: "Inputs and Actions",
    descriptor: giveMe.aDataContainer({
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
      }),
      actions: [
        giveMe.aButton({ label: "ACTION_1" }),
        giveMe.aButton({ label: "ACTION_2", icon: "pi pi-heart" })
      ]
    })
  }
];
</script>