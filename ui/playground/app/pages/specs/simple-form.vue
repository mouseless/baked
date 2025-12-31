<template>
  <UiSpec
    title="Simple Form"
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
      inputs: [
        giveMe.anInput({
          name: "text",
          component: giveMe.anExpectedInput({ testId: "input" }),
          required: true
        })
      ],
      submit: giveMe.aButton({ label: "Spec: Submit" }).schema,
      title: "Spec: Simple Form"
    })
  },
  {
    name: "Multiple Inputs",
    descriptor: giveMe.aSimpleForm({
      action: giveMe.aLocalAction({ showMessage: "ok" }),
      inputs: [
        giveMe.anInput({
          name: "param-1",
          component: giveMe.anExpectedInput()
        }),
        giveMe.anInput({
          name: "param-2",
          component: giveMe.anExpectedInput()
        }),
        giveMe.anInput({
          name: "param-3",
          component: giveMe.anExpectedInput()
        })
      ]
    })
  },
  {
    name: "Dialog",
    descriptor: giveMe.aSimpleForm({
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
      dialogOptions: giveMe.aSimpleFormDialog({
        open: giveMe.aButton({ label: "Spec: Simple Form" }).schema,
        cancel: giveMe.aButton({ label: "Spec: Cancel" }).schema,
        message: "Spec: Message"
      }),
      inputs: [
        giveMe.anInput({
          name: "text",
          component: giveMe.anExpectedInput({ testId: "input" }),
          required: true
        })
      ],
      submit: giveMe.aButton({ label: "Spec: Submit" }).schema,
      title: "Spec: Simple Form"
    })
  }
];
</script>
