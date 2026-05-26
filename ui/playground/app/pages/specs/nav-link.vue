<template>
  <UiSpec :variants />
</template>
<script setup>
import giveMe from "@utils/giveMe";

const variants = [
  {
    name: "Base",
    descriptor: giveMe.aNavLink({
      icon: "pi pi-eye",
      path: "/specs",
      data: giveMe.anInlineData("Link")
    })
  },
  {
    name: "Dynamic",
    descriptor: giveMe.aNavLink({
      path: "/test-path/[id]",
      query: giveMe.anInlineData({ query: "value" }),
      params: giveMe.anInlineData({ id: "test-id" })
    })
  },
  {
    name: "Inline Error",
    descriptor: giveMe.aNavLink({
      path: "/test-path/[id]",
      params: giveMe.aRemoteData({ path: "/exception-samples/handled" })
    })
  }
];
</script>