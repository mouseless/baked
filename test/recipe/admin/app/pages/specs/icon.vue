<template>
  <UiSpec
    title="Icon"
    :variants="variants"
    :no-loading-variant="true"
  />
</template>
<script setup>
import giveMe from "~/utils/giveMe";

const variants = [
  {
    name: "Base",
    descriptor: giveMe.anIcon({ iconClass: "pi-wave-pulse" })
  }
];
</script>
