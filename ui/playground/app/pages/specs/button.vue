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
        giveMe.aLocalAction({ showMessage: "Confirmed!" })
      ]),
      label: "Spec: Button",
      dialogOptions: giveMe.aButtonDialog({
        header: "Spec: Header",
        content: giveMe.anExpected({
          testId: "content",
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
      action: null,
      label: "Spec: Button",
      dialogOptions: giveMe.aButtonDialog({
        dismissableMask: false,
        form: giveMe.aSimpleForm({
          action: giveMe.aCompositeAction([
            giveMe.aLocalAction({
              composable: "useShowMessage",
              options: giveMe.aContextData({
                key: "model",
                prop: "input",
                targetProp: "message"
              })
            }),
            giveMe.aLocalAction({ delay: 500 }), // wait for toast to show
            giveMe.aPublishAction({ event: "close-dialog" })
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