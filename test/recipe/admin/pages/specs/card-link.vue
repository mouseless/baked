<template>
  <UiSpec
    title="Card Link"
    :variants="variants"
    :no-loading-variant="true"
  />
</template>
<script setup>
import giveMe from "~/utils/giveMe";

// TODO - use 'Spec.' prefix for localization keys
// in all test setups 
const variants = [
  {
    name: "Base",
    descriptor: giveMe.aCardLink({
      route: "/some-route",
      icon: "pi pi-wave-pulse",
      title: "Spec.Title",
      description: "Spec.Description"
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
      disabledReason: "Spec.Soon"
    })
  }
];
</script>
