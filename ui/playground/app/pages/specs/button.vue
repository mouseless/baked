<template>
  <UiSpec
    title="Button"
    :variants="variants"
    :no-loading-variant="true"
  />
</template>
<script setup>
import giveMe from "@utils/giveMe";

const variants = [
  {
    name: "Base",
    descriptor: giveMe.aButton({
      action: {
        type: "Composite",
        parts: [
          {
            type: "Local",
            composable: "useDelay",
            options: {
              type: "Inline",
              value: { time: 100 }
            }
          },
          {
            type: "Local",
            composable: "useShowMessage",
            options: {
              type: "Inline",
              value: { message: "Execute Action" }
            }
          }
        ]
      },
      label: "Spec: Button",
      icon: "pi pi-play-circle"
    })
  }
];
</script>