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
      validateComposables: ["mustSkipThisComposables"],
      action: giveMe.aLocalAction({ showMessage: "ok" }),
      inputs: [
        giveMe.anInput({
          name: "param-1",
          required: true,
          component: giveMe.anInputText({})
        }),
        giveMe.anInput({
          name: "param-2",
          component: giveMe.anInputNumber({})
        }),
        giveMe.anInput({
          name: "param-3",
          component: giveMe.aSelectButton({})
        }),
        giveMe.anInput({
          component: giveMe.aSelect({
            allowEmpty: true
          })
        })
      ]
    })
  }
];
</script>