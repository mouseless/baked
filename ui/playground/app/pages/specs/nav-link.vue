<template>
  <UiSpec
    title="Spec: NavLink"
    :variants="variants"
  />
</template>
<script setup>
import giveMe from "@utils/giveMe";

const variants = [
  {
    name: "Base",
    descriptor: giveMe.aNavLink({
      icon: "pi pi-eye",
      path: "/test-path/{id}",
      idProp: "id",
      textProp: "name",
      data: {
        id: "test-id",
        name: "Spec: Name"
      }
    })
  }
];
</script>
