<template>
  <ComponentSpec
    title="Card Link"
    :variants="variants"
  />
</template>
<script setup>
import { giveMe } from "#imports";

const variants = [
  {
    name: "Base",
    descriptor: giveMe.aCardLink({
      route: "/some-route",
      icon: "pi pi-wave-pulse",
      title: "CARD TITLE",
      description: "CARD DESCRIPTION"
    })
  },
  {
    name: "Only Mandatory Fields",
    descriptor: giveMe.aCardLink({
      icon: null,
      description: null
    })
  },
  {
    name: "Disabled",
    descriptor: giveMe.aCardLink({
      disabled: true,
      disabledReason: "SOON"
    })
  }
];
</script>
