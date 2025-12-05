<template>
  <UiSpec
    title="SimpleForm"
    :variants="variants"
    no-loading-variant
  />
</template>
<script setup>
import giveMe from "@utils/giveMe";

const variants = [
  {
    name: "Base",
    descriptor: giveMe.aSimpleForm({
      action: {
        type: "Composite",
        parts: [
          {
            type: "Local",
            composable: "useDelay",
            options: {
              type: "Inline",
              value: { time: 500 }
            }
          },
          {
            type: "Local",
            composable: "useShowMessage",
            options: {
              type: "Context",
              key: "ModelData",
              prop: "text",
              targetProp: "message"
            }
          }
        ]
      },
      buttonLabel: "Submit",
      inputs: [
        giveMe.anInput({
          name: "text",
          component: giveMe.anInputText(),
          required: true
        })
      ]
    })
  }
];
</script>
