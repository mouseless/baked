<template>
  <UiSpec
    title="Modal"
    :variants="variants"
    no-loading-variant
  />
</template>
<script setup>
import giveMe from "@utils/giveMe";

const variants = [
  {
    name: "Base",
    descriptor: giveMe.aModal({
      content: giveMe.aText( { value: "Modal Content" }),
      footer: giveMe.aText({ value: "Modal Footer" }),
      header: giveMe.aText({ value: "Modal Header" }),
      label: "Spec: Modal Button"
    })
  },
  {
    name: "Form",
    descriptor: giveMe.aModal({
      content: giveMe.aSimpleForm({
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
      }),
      label: "Form"
    })
  }
];
</script>