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
      header: "Spec: Dialog Header",
      open: giveMe.aButton({ label: "Spec: Dialog Button" }).schema
    })
  },
  {
    name: "Action",
    descriptor: giveMe.aDialog({
      action: giveMe.aCompositeAction([
        giveMe.aLocalAction({ delay: 100 }),
        giveMe.aLocalAction({ showMessage: "Execute Action" })
      ]),
      content: giveMe.aText( { value: "Dialog Content" }),
      header: "Spec: Dialog Header",
      submit: giveMe.aButton({ label: "Spec: Dialog Action" })
    })
  }
];
</script>