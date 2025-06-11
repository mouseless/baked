<template>
  <UiSpec
    title="Card Link"
    :variants="variants"
    :no-loading-variant="true"
  />
</template>
<script setup>
import giveMe from "~/utils/giveMe";

const variants = [
  {
    name: "Base",
    descriptor: giveMe.aCardLink({
      route: "/some-route",
      icon: "pi pi-wave-pulse",
      title: "CARD_TITLE",
      description: "CARD_DESCRIPTION"
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
