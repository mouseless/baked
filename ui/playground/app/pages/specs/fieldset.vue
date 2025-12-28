<template>
  <UiSpec
    title="Fieldset"
    :variants
  />
</template>
<script setup>
import giveMe from "@utils/giveMe";

const variants = [
  {
    name: "Base",
    descriptor: giveMe.aFieldset({
      titleProp: "label",
      fields: [
        giveMe.aField({
          label: "Spec: Data 1",
          key: "data1",
          component: giveMe.anExpected({ testId: "prop-1", data: giveMe.aContextData({ parent: "data.data1" }) })
        }),
        giveMe.aField({
          label: "Spec: Data 2",
          key: "data2",
          component: giveMe.anExpected({ testId: "prop-2", data: giveMe.aContextData({ parent: "data.data2" }) })
        })
      ],
      data: giveMe.anInlineData({
        label: "Title",
        data1: "Value 1",
        data2: "Value 2"
      })
    })
  }
];
</script>
