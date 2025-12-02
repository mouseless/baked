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
        type: "Local",
        composable: "useShowMessage",
        options: {
          type: "Context",
          key: "ModelData",
          prop: "text",
          targetProp: "message"
        }
      },
      inputs: [
        giveMe.anInput({
          name: "text",
          component: giveMe.anInputText()
        }),
        giveMe.anInput({
          name: "number",
          component: giveMe.anInputNumber()
        }),
        giveMe.anInput({
          name: "select",
          component: giveMe.aSelect({
            label: "Label",
            data: ["OPTION_1", "OPTION_2"]
          })
        })
      ],
      label: "Submit"
    })
  }
];
</script>
