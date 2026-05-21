<template>
  <UiSpec
    :variants
    use-model
    no-loading-variant
    no-validation-variant
  />
</template>
<script setup>
import giveMe from "@utils/giveMe";

const variants = [
  {
    name: "Base",
    descriptor: giveMe.anInputText({
      label: giveMe.aLabel({
        showOptionality: true
      })
    })
  },
  {
    name: "Select: No Label Mode",
    descriptor: giveMe.aSelect({
      label: giveMe.aLabel({
        mode: null
      })
    })
  },
  {
    name: "Select: Float:On Mode",
    descriptor: giveMe.aSelect({
      label: giveMe.aLabel({
        text: "Float:On Mode"
      })
    })
  },
  {
    name: "Select: Ifta Mode",
    descriptor: giveMe.aSelect({
      label: giveMe.aLabel({
        text: "Ifta Mode",
        mode: "ifta"
      })
    })
  },
  {
    name: "Select: Float:In Mode",
    descriptor: giveMe.aSelect({
      label: giveMe.aLabel({
        text: "Float:In Mode",
        variant: "in"
      })
    })
  },
  {
    name: "SelectButton: Ifta Mode",
    descriptor: giveMe.aSelectButton({
      label: giveMe.aLabel({
        text: "Ifta Mode",
        mode: "ifta"
      })
    })
  },
  {
    name: "SelectButton: No Label Mode",
    descriptor: giveMe.aSelectButton({
      label: giveMe.aLabel({
        label: null,
        mode: null
      })
    })
  },
  {
    name: "InputText: No Label Mode",
    descriptor: giveMe.anInputText({
      label: giveMe.aLabel({
        mode: null
      })
    })
  },
  {
    name: "InputText: Float:On Mode",
    descriptor: giveMe.anInputText({
      label: giveMe.aLabel({
        text: "Float:On Mode"
      })
    })

  },
  {
    name: "InputText: Float:In Mode",
    descriptor: giveMe.anInputText({
      label: giveMe.aLabel({
        text: "Float:In Mode",
        variant: "in"
      })
    })
  },
  {
    name: "InputText: Ifta Mode",
    descriptor: giveMe.anInputText({
      label: giveMe.aLabel({
        text: "Ifta Mode",
        mode: "ifta"
      })
    })
  },
  {
    name: "InputNumber: No Label Mode",
    descriptor: giveMe.anInputNumber({
      label: giveMe.aLabel({
        mode: null
      })
    })
  },
  {
    name: "InputNumber: Float:On Mode",
    descriptor: giveMe.anInputNumber({
      label: giveMe.aLabel({
        text: "Float:On Mode"
      })
    })
  },
  {
    name: "InputNumber: Float:In Mode",
    descriptor: giveMe.anInputNumber({
      label: giveMe.aLabel({
        text: "Float:In Mode",
        variant: "in"
      })
    })
  },
  {
    name: "InputNumber: Ifta Mode",
    descriptor: giveMe.anInputNumber({
      label: giveMe.aLabel({
        text: "Ifta Mode",
        mode: "ifta"
      })
    })
  }
];
</script>