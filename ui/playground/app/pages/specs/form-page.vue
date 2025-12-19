<template>
  <UiSpec
    title="Form Page"
    :variants="variants"
    no-loading-variant
  />
</template>
<script setup>
import giveMe from "@utils/giveMe";

const variants = [
  {
    name: "Base",
    descriptor: giveMe.aFormPage({
      action: giveMe.aCompositeAction([
        giveMe.aLocalAction({ delay: 100 }),
        giveMe.aLocalAction({
          composable: "useShowMessage",
          options: giveMe.aContextData({
            key: "model",
            prop: "text",
            targetProp: "message"
          })
        })
      ]),
      title: "Spec: Title",
      description: "Spec: Description",
      button: giveMe.aButton({ label: "Spec: Submit" }),
      inputs: [
        giveMe.anInput({
          name: "text",
          component: giveMe.anInputText(),
          required: true
        })
      ]
    })
  },
  {
    name: "Multiple Inputs",
    descriptor: giveMe.aFormPage({
      action: giveMe.aLocalAction({ showMessage: "ok" }),
      inputs: [
        giveMe.anInput({
          name: "param-1",
          component: giveMe.anInputText()
        }),
        giveMe.anInput({
          name: "param-2",
          component: giveMe.anInputText()
        }),
        giveMe.anInput({
          name: "param-3",
          component: giveMe.anInputText()
        })
      ]
    })
  }
];
</script>
