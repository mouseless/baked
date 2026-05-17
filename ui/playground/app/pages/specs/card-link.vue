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
    descriptor: giveMe.aCardLink({
      route: "/some-route",
      icon: "pi pi-wave-pulse",
      title: "Spec: Title",
      description: "Spec: Description"
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
      disabledReason: "Spec: Soon"
    })
  }
];
</script>