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
      submit: giveMe.aButton({ label: "Spec: Submit" }).schema,
      inputs: [
        giveMe.anInput({
          name: "text",
          component: giveMe.anExpectedInput({ testId: "input" }),
          defaultValue: "default",
          required: true
        })
      ]
    })
  },
  {
    name: "Error",
    descriptor: giveMe.aFormPage({
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
    descriptor: giveMe.aFormPage({
      action: giveMe.aLocalAction({ showMessage: "ok" }),
      inputs: [
        giveMe.anInput({ name: "param-1" }),
        giveMe.anInput({ name: "param-2" }),
        giveMe.anInput({ name: "param-3" })
      ]
    })
  },
  {
    name: "Sections",
    descriptor: giveMe.aFormPage({
      action: giveMe.aLocalAction({ showMessage: "ok" }),
      sections: [
        giveMe.aFormPageSection({
          key: "section-1",
          label: "Spec: Section 1"
        }),
        giveMe.aFormPageSection({
          key: "section-2",
          label: "Spec: Section 2"
        })
      ]
    })
  },
  {
    name: "Validations",
    descriptor: giveMe.aFormPage({
      action: giveMe.aLocalAction({ showMessage: "ok" }),
      inputs: [
        giveMe.anInput({ name: "param-1", required: true }),
        giveMe.anInput({ name: "param-2" })
      ]
    })
  },
  {
    name: "Layout Options",
    descriptor: giveMe.aFormPage({
      action: giveMe.aLocalAction({ showMessage: "ok" }),
      sections: [
        giveMe.aFormPageSection({
          inputGroups: [
            giveMe.aFormPageInputGroup({
              inputs: [
                giveMe.anInput({ name: "narrow-1" }),
                giveMe.anInput({ name: "narrow-1-2" })
              ]
            }),
            giveMe.aFormPageInputGroup({ inputs: [giveMe.anInput({ name: "narrow-2" })] }),
            giveMe.aFormPageInputGroup({ inputs: [giveMe.anInput({ name: "narrow-3" })] }),
            giveMe.aFormPageInputGroup({ inputs: [giveMe.anInput({ name: "narrow-4" })] }),
            giveMe.aFormPageInputGroup({
              inputs: [giveMe.anInput({ name: "wide-1" })],
              wide: true
            }),
            giveMe.aFormPageInputGroup({
              inputs: [
                giveMe.anInput({ name: "wide-2" }),
                giveMe.anInput({ name: "wide-2-2" }),
                giveMe.anInput({ name: "wide-2-3" })
              ],
              wide: true
            }),
            giveMe.aFormPageInputGroup({
              inputs: [
                giveMe.anInput({
                  name: "narrow-trailing",
                  component: giveMe.anInputText( {
                    label: giveMe.aLabel({ text: "Narrow-trailing", mode: "ifta" })
                  })
                }),
                giveMe.anInput({
                  name: "select",
                  component: giveMe.aSelect({
                    label: giveMe.aLabel({ text: "Select", mode: "ifta" })
                  })
                }),
                giveMe.anInput({
                  name: "select-button",
                  component: giveMe.aSelectButton({
                    label: giveMe.aLabel({ text: "Select Button", mode: "ifta" })
                  })
                })
              ],
              wide: true
            })
          ]
        })
      ]
    })
  }
];
</script>