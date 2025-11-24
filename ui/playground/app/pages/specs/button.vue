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
            composable: "useShowMessage",
            args: [
              { message: "Execute Action" }
            ]
          },
          {
            type: "Local",
            composable: "useDelay",
            args:[
              500
            ]
          },
          {
            type: "Remote",
            path: "/rich-transient-with-datas/{id}/method",
            method: "POST",
            headers: {
              type: "Inline",
              value: {
                Authorization: "token-admin-ui"
              }
            },
            query: {
              type: "Inline",
              value: {
                val: "2"
              }
            },
            params: {
              type: "Inline",
              value: {
                id: 1
              }
            },
            body: {
              type: "Inline",
              value: {
                text: "text"
              }
            }
          }
        ]
      },
      postAction: {
        type: "Local",
        composable: "useShowMessage",
        args: [
          { message: "Execute Post Action" }
        ]
      },
      label: "Spec: Button",
      icon: "pi-play-circle"
    })
  }
];
</script>