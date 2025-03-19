<template>
  <UiSpec
    title="Link"
    :variants="variants"
  />
</template>
<script setup>
import giveMe from "~/utils/giveMe";

const variants = [
  {
    name: "Base",
    descriptor: giveMe.aLink({
      path: "/test-path/{0}",
      idProp: "id",
      textProp: "name",
      data: {
        id: "test-id",
        name: "TEST"
      }
    })
  }
];
</script>
