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
      action: giveMe.aCompositeAction([
        giveMe.aLocalAction({ delay: 500 }),
        giveMe.aLocalAction({
          composable: "useShowMessage",
          options: giveMe.aContextData({
            key: "model",
            prop: "text",
            targetProp: "message"
          })
        })
      ]),
      buttonLabel: "Spec: SimpleFormButton",
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
