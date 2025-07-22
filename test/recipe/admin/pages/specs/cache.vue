<template>
  <UiSpec
    title="Cache"
    :variants="variants"
    :no-loading-variant="true"
  />
</template>
<script setup>
import giveMe from "~/utils/giveMe";

const variants = [
  {
    name: "Application",
    descriptor: giveMe.anExpected({
      data: {
        type: "Remote",
        path: "/cache-samples/application",
        options: { "client-cache": "application" }
      }
    })
  },
  {
    name: "User",
    descriptor: giveMe.anExpected({
      data: {
        type: "Remote",
        path: "/cache-samples/scoped",
        options: { "client-cache": "user" }
      }
    })
  }
];
</script>
