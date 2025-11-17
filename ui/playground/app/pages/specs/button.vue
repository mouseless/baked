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
            args: {
              time: "100"
            }
          },
          {
            type: "Local",
            composable: "useShowMessage",
            args: {
              message: "Message from button click"
            }
          }
        ]
      },
      label: "Base",
      icon: "pi-play-circle"
    })
  },
  {
    name: "Remote",
    descriptor: giveMe.aButton({
      action: {
        type: "Remote",
        path: "/rich-transient-with-datas/{id}/method",
        method: "POST",
        params: {
          type: "Inline",
          value: {
            id: 1
          }
        }
      },
      label: "Remote"
    })
  }
];
</script>