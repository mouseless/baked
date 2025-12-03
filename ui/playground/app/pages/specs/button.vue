<template>
  <UiSpec
    title="Button"
    :variants="variants"
    :no-loading-variant="true"
  />
</template>
<script setup>
import { useContext } from "#imports";
import giveMe from "@utils/giveMe";

const context = useContext();

context.provideData({ id: 12 }, "ParentData");

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
            options: {
              type: "Inline",
              value: { message: "Execute Action" }
            }
          },
          {
            type: "Local",
            composable: "useDelay",
            options: {
              type: "Inline",
              value: { time: 100 }
            }
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
              type: "Computed",
              composable: "useNuxtRoute",
              options: {
                type: "Inline",
                value:{ property: "query" }
              }
            },
            params: {
              type: "Context",
              key: "ParentData"
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
        options: {
          type: "Inline",
          value: { message: "Execute Post Action" }
        }
      },
      label: "Spec: Button",
      icon: "pi pi-play-circle"
    })
  }
];
</script>