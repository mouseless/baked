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
    descriptor: giveMe.aDialog({
      content: giveMe.anExpected( { testId: "content", value: "Dialog Content" }),
      header: "Spec: Dialog Header",
      open: giveMe.aButton({ label: "Spec: Dialog Button" }).schema
    })
  }
];
</script>