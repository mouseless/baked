<template>
  <UiSpec
    title="Message"
    :variants="variants"
  />
</template>
<script setup>
import giveMe from "~/utils/giveMe";

const variants = [
  {
    name: "Base",
    descriptor: giveMe.aMessage({
      message: "Message",
      icon: "pi-info-circle",
      severity: "info"
    })
  },
  {
    name: "No icon",
    descriptor: giveMe.aMessage({
      icon: null
    })
  },
  {
    name: "Info",
    descriptor: giveMe.aMessage({
      message: "Message",
      icon: "pi-info-circle",
      severity: "info"
    })
  },
  {
    name: "Warning",
    descriptor: giveMe.aMessage({
      severity: "warn"
    })
  },
  {
    name: "Error",
    descriptor: giveMe.aMessage({
      severity: "error"
    })
  }
];
</script>
