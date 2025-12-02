<template>
  <UiSpec
    title="SimpleForm"
    :variants="variants"
    :no-loading-variant="true"
  />
</template>
<script setup>
import giveMe from "@utils/giveMe";

const variants = [
  {
    name: "Base",
    descriptor: giveMe.aSimpleForm({
      action: {
        type: "Remote",
        path: "/form-sample/states",
        method: "POST",
        headers: {
          type: "Inline",
          value: {
            Authorization: "token-admin-ui"
          }
        },
        body: {
          type: "Injected",
          key: "Model"
        }
      },
      inputs: [
        giveMe.anInput({
          name: "text",
          component: giveMe.anInputText()
        }),
        giveMe.anInput({
          name: "number",
          component: giveMe.anInputNumber()
        }),
        giveMe.anInput({
          name: "select",
          component: giveMe.aSelect({
            label: "Label",
            data: ["OPTION_1", "OPTION_2"]
          })
        })
      ],
      label: "Submit"
    })
  }
];
</script>
