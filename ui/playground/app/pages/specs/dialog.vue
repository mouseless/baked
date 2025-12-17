<template>
  <UiSpec
    title="Dialog"
    :variants="variants"
    no-loading-variant
  />
</template>
<script setup>
import giveMe from "@utils/giveMe";

const variants = [
  {
    name: "Base",
    descriptor: giveMe.aDialog({
      content: giveMe.aText( { value: "Dialog Content" }),
      dialogButton: { label: "Spec: Dialog Button" },
      header: "Spec: Dialog Header"
    })
  },
  {
    name: "Action",
    descriptor: giveMe.aDialog({
      action: giveMe.aCompositeAction([
        giveMe.aLocalAction({ delay: 100 }),
        giveMe.aLocalAction({ showMessage: "Execute Action" })
      ]),
      actionButton: { label: "Spec: Dialog Action" },
      content: giveMe.aText( { value: "Dialog Content" }),
      header: "Spec: Dialog Header"
    })
  }
];
</script>