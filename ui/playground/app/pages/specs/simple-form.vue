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
    descriptor: giveMe.aSimpleForm({
      action: giveMe.aCompositeAction([
        giveMe.aLocalAction({ delay: 100 }),
        giveMe.aLocalAction({
          composable: "useShowMessage",
          options: giveMe.aContextData({
            key: "model",
            prop: "input",
            targetProp: "message"
          })
        })
      ]),
      inputs: [
        giveMe.anInput({
          name: "input",
          required: true
        })
      ],
      submit: giveMe.aButton({ label: "Spec: Submit" }).schema,
      title: "Spec: Simple Form"
    })
  },
  {
    name: "Error",
    descriptor: giveMe.aSimpleForm({
      action: giveMe.aRemoteAction({ path: "/exception-samples/handled" }),
      title: "Spec: Title",
      description: "Spec: Description",
      submit: giveMe.aButton({ label: "Spec: Submit" }).schema,
      inputs: [
        giveMe.anInput({
          name: "text",
          component: giveMe.anExpectedInput({ testId: "input" })
        })
      ]
    })
  },
  {
    name: "Multiple Inputs",
    descriptor: giveMe.aSimpleForm({
      action: giveMe.aLocalAction({ showMessage: "ok" }),
      inputs: [
        giveMe.anInput({ name: "param-1" }),
        giveMe.anInput({ name: "param-2" }),
        giveMe.anInput({ name: "param-3" })
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
          name: "input",
          required: true
        })
      ],
      submit: giveMe.aButton({ label: "Spec: Submit" }).schema,
      title: "Spec: Simple Form"
    })
  },
  {
    name: "Dialog Error",
    descriptor: giveMe.aSimpleForm({
      action: giveMe.aRemoteAction({ path: "/exception-samples/handled" }),
      title: "Spec: Title",
      description: "Spec: Description",
      submit: giveMe.aButton({ label: "Spec: Submit" }).schema,
      dialogOptions: giveMe.aSimpleFormDialog({
        open: giveMe.aButton({ label: "Spec: Simple Form" }).schema,
        cancel: giveMe.aButton({ label: "Spec: Cancel" }).schema,
        message: "Spec: Message"
      }),
      inputs: [
        giveMe.anInput({
          name: "text",
          component: giveMe.anExpectedInput({ testId: "input" })
        })
      ]
    })
  },
  {
    name: "Validation",
    descriptor: giveMe.aSimpleForm({
      action: giveMe.aLocalAction({ showMessage: "ok" }),
      inputs: [
        giveMe.anInput({
          name: "input-1",
          numeric: true,
          required: true
        }),
        giveMe.anInput({
          name: "input-2",
          numeric: true
        })
      ]
    })
  }
];
</script>