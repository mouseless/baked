<template>
  <UiSpec
    title="Bake"
    :variants="variants"
  />
</template>
<script setup>
import giveMe from "~/utils/giveMe";

const variants = [
  {
    name: "Base",
    descriptor: giveMe.aString({ value: "TEST" })
  },
  {
    name: "Parent Data",
    descriptor: giveMe.aContainer({
      contents: [
        giveMe.anExpected({
          testId: "child-root",
          data: { type: "Injected", key: "ParentData" }
        }),
        giveMe.anExpected({
          testId: "child-prop",
          data: { type: "Injected", key: "ParentData", prop: "child" }
        })
      ],
      data: { type: "Inline", value: { child: "CHILD VALUE" } }
    })
  }
];
</script>
