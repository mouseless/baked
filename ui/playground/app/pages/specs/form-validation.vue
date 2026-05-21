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
        })
      ]
    })
  }
];
</script>