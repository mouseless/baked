<template>
  <UiSpec
    title="NavLink"
    :variants="variants"
  />
</template>
<script setup>
import giveMe from "~/utils/giveMe";

const variants = [
  {
    name: "Base",
    descriptor: giveMe.aNavLink({
      path: "/test-path/{0}",
      idProp: "id",
      textProp: "name",
      data: {
        id: "test-id",
        name: "Navlink.Name"
      }
    })
  }
];
</script>
