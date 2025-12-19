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
      name: "Spec: Simple Form",
      inputs: [
        giveMe.anInput({
          name: "text",
          component: giveMe.anInputText(),
          required: true
        })
      ],
      submitButton: giveMe.aButtonSchema({ label: "Spec: Submit" })
    })
  },
  {
    name: "Dialog",
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
      dialogCancelButton: giveMe.aButtonSchema({ label: "Spec: Cancel" }),
      dialogToggleButton: giveMe.aButtonSchema({ label: "Spec: Toggle Dialog" }),
      dialog: true,
      name: "Spec: Simple Form",
      inputs: [
        giveMe.anInput({
          name: "text",
          component: giveMe.anInputText(),
          required: true
        })
      ],
      submitButton: giveMe.aButtonSchema({ label: "Spec: Submit" })
    })
  }
];
</script>
