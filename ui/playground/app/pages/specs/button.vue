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
    descriptor: giveMe.aButton({
      action: giveMe.aCompositeAction([
        giveMe.aLocalAction({ delay: 100 }),
        giveMe.aLocalAction({ showMessage: "Execute Action" })
      ]),
      label: "Spec: Button",
      icon: "pi pi-play-circle"
    })
  },
  {
    name: "Small",
    descriptor: giveMe.aButton({
      action: giveMe.aCompositeAction([
        giveMe.aLocalAction({ delay: 100 }),
        giveMe.aLocalAction({ showMessage: "Execute Action" })
      ]),
      label: "Spec: Button Small",
      icon: "pi pi-play-circle",
      size: "small"
    })
  },
  {
    name: "Large",
    descriptor: giveMe.aButton({
      action: giveMe.aCompositeAction([
        giveMe.aLocalAction({ delay: 100 }),
        giveMe.aLocalAction({ showMessage: "Execute Action" })
      ]),
      label: "Spec: Button Large",
      icon: "pi pi-play-circle",
      size: "large"
    })
  },
  {
    name: "Dialog",
    descriptor: giveMe.aButton({
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
      label: "Spec: Button",
      dialogOptions: giveMe.aButtonDialog({
        header: "Spec: Header",
        content: giveMe.aText({
          data: giveMe.aComputedData({
            composable: "useLocalization",
            options: giveMe.anInlineData({ key: "Spec: Message" })
          })
        }),
        submit: giveMe.aButton({
          label: "Spec: Submit",
          severity: "primary"
        }).schema,
        cancel: giveMe.aButton({ label: "Spec: Cancel" }).schema
      })
    })
  },
  {
    name: "Dialog Form",
    descriptor: giveMe.aButton({
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
      label: "Spec: Button",
      dialogOptions: giveMe.aButtonDialog({
        dismissableMask: false,
        form: giveMe.aSimpleForm({
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
              name: "input",
              required: true
            })
          ],
          submit: giveMe.aButton({
            label: "Spec: Submit",
            severity: "primary"
          }).schema,
          title: null
        }),
        cancel: giveMe.aButton({ label: "Spec: Cancel" }).schema
      })
    })
  },
  {
    name: "Dialog Form Error",
    descriptor: giveMe.aButton({
      dialogOptions: giveMe.aButtonDialog({
        form: giveMe.aSimpleForm({
          action: giveMe.aRemoteAction({ path: "/exception-samples/handled" }),
          submit: giveMe.aButton({ label: "Spec: Submit" }).schema,
          inputs: [giveMe.anInput()],
          title: null
        }),
        cancel: null
      })
    })
  }
];
</script>