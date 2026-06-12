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
  }
];
</script>