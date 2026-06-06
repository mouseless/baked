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
      validations: ["useFakeValidation"],
      action: giveMe.aLocalAction({ showMessage: "ok" }),
      inputs: [
        giveMe.anInput({
          name: "param-1",
          required: true,
          component: giveMe.anInputText()
        }),
        giveMe.anInput({
          name: "param-2",
          component: giveMe.anInputText({
            label: giveMe.aLabel({
              showOptionality: true
            })
          })
        }),
        giveMe.anInput({
          name: "param-3",
          component: giveMe.anExpectedInput({
            testId: "param-3"
          })
        })
      ]
    })
  },
  {
    name: "Mutable",
    descriptor: giveMe.aSimpleForm({
      validations: ["useFakeValidation"],
      showValidateSummary: true,
      action: giveMe.aLocalAction({ showMessage: "ok" }),
      inputs: [
        giveMe.anInput({
          required: false,
          name: "input-1",
          component: giveMe.anExpectedInput({
            testId: "input-1",
            restrictedValue: "error",
            hint: "Type 'error' to see error message"
          })
        }),
        giveMe.anInput({
          required: true,
          name: "input-2",
          component: giveMe.anExpectedInput({
            testId: "input-2"
          })
        })
      ]
    })
  },
  {
    name: "Nested Validation",
    descriptor: giveMe.aSimpleForm({
      validations: ["useFakeValidation"],
      action: giveMe.aLocalAction({ showMessage: "ok" }),
      inputs: [
        giveMe.anInput({
          name: "input-group",
          component: giveMe.anExpectedInputGroup({ testId: "input-group" })
        })
      ]
    })
  }
];
</script>