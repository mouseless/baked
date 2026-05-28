<template>
  <UiSpec :variants />
</template>
<script setup>
import giveMe from "@utils/giveMe";

const variants = [
  {
    name: "Base",
    descriptor: giveMe.aMessage({
      message: "Message",
      icon: "pi pi-info-circle",
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
    name: "No data",
    descriptor: giveMe.aMessage({
      data: null
    })
  },
  {
    name: "Info",
    descriptor: giveMe.aMessage({
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
  },
  {
    name: "Success",
    descriptor: giveMe.aMessage({
      severity: "success"
    })
  },
  {
    name: "Secondary",
    descriptor: giveMe.aMessage({
      severity: "secondary"
    })
  },
  {
    name: "Contrast",
    descriptor: giveMe.aMessage({
      severity: "contrast"
    })
  },
  {
    name: "Simple Variant",
    descriptor: giveMe.aMessage({
      variant: "simple"
    })
  }
];
</script>