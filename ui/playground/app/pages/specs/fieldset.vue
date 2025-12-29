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
        }),
        giveMe.aField({
          label: "Spec: Data 3",
          key: "data3",
          wide: true,
          component: giveMe.anExpected({ testId: "prop-3", data: giveMe.aContextData({ parent: "data.data3" }) })
        })
      ],
      data: giveMe.anInlineData({
        label: "Title",
        data1: "Value 1",
        data2: "Value 2",
        data3: String.raw`
        Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod
        tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim
        veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea
        commodo consequat. Duis aute irure dolor in reprehenderit in voluptate
        velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint
        occaecat cupidatat non proident, sunt in culpa qui officia deserunt
        mollit anim id est laborum.
        `
      })
    })
  }
];
</script>
