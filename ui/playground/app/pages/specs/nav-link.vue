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
      path: "/specs",
      data: giveMe.anInlineData("Base")
    })
  },
  {
    name: "Dynamic",
    descriptor: giveMe.aNavLink({
      icon: "pi pi-eye",
      path: "/test-path/{id}",
      query: giveMe.anInlineData({ query: "value" }),
      params: giveMe.aCompositeData(
        [
          giveMe.anInlineData({ id: "test-id" })
        ]
      ),
      data: giveMe.anInlineData("Params")
    })
  }
];
</script>
