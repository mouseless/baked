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
    name: "Validate Label",
    descriptor: giveMe.anInputText({
      labeler: giveMe.aLabeler({
        validateLabel: true
      })
    })
  },
  {
    name: "Select: No Label Mode",
    descriptor: giveMe.aSelect({
      labeler: giveMe.aLabeler({
        labelMode: null
      })
    })
  },
  {
    name: "Select: Float:On Mode",
    descriptor: giveMe.aSelect({
      labeler: giveMe.aLabeler({
        label: "Float:On Mode"
      })
    })
  },
  {
    name: "Select: Ifta Mode",
    descriptor: giveMe.aSelect({
      labeler: giveMe.aLabeler({
        label: "Ifta Mode",
        labelMode: "ifta"
      })
    })
  },
  {
    name: "Select: Float:In Mode",
    descriptor: giveMe.aSelect({
      labeler: giveMe.aLabeler({
        label: "Float:In Mode",
        labelVariant: "in"
      })
    })
  },
  {
    name: "SelectButton: Ifta Mode",
    descriptor: giveMe.aSelectButton({
      labeler: giveMe.aLabeler({
        label: "Ifta Mode",
        labelMode: "ifta"
      })
    })
  },
  {
    name: "SelectButton: No Label Mode",
    descriptor: giveMe.aSelectButton({
      labeler: giveMe.aLabeler({
        label: null,
        labelMode: null
      })
    })
  },
  {
    name: "InputText: No Label Mode",
    descriptor: giveMe.anInputText({
      labeler: giveMe.aLabeler({
        labelMode: null
      })
    })
  },
  {
    name: "InputText: Float:On Mode",
    descriptor: giveMe.anInputText({
      labeler: giveMe.aLabeler({
        label: "Float:On Mode"
      })
    })

  },
  {
    name: "InputText: Float:In Mode",
    descriptor: giveMe.anInputText({
      labeler: giveMe.aLabeler({
        label: "Float:In Mode",
        labelVariant: "in"
      })
    })
  },
  {
    name: "InputText: Ifta Mode",
    descriptor: giveMe.anInputText({
      labeler: giveMe.aLabeler({
        label: "Ifta Mode",
        labelMode: "ifta"
      })
    })
  },
  {
    name: "InputNumber: No Label Mode",
    descriptor: giveMe.anInputNumber({
      labeler: giveMe.aLabeler({
        labelMode: null
      })
    })
  },
  {
    name: "InputNumber: Float:On Mode",
    descriptor: giveMe.anInputNumber({
      labeler: giveMe.aLabeler({
        label: "Float:On Mode"
      })
    })
  },
  {
    name: "InputNumber: Float:In Mode",
    descriptor: giveMe.anInputNumber({
      labeler: giveMe.aLabeler({
        label: "Float:In Mode",
        labelVariant: "in"
      })
    })
  },
  {
    name: "InputNumber: Ifta Mode",
    descriptor: giveMe.anInputNumber({
      labeler: giveMe.aLabeler({
        label: "Ifta Mode",
        labelMode: "ifta"
      })
    })
  }
];
</script>