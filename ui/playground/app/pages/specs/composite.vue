<template>
  <UiSpec
    title="Composite"
    :variants
    no-loading-variant
  />
</template>
<script setup>
import giveMe from "@utils/giveMe";

const variants = [
  {
    name: "Base",
    descriptor: giveMe.aComposite([
      giveMe.anExpected({ testId: "component-1", value: "COMPONENT_1" }),
      giveMe.anExpected({ testId: "component-2", value: "COMPONENT_2" })
    ])
  }
];
</script>
