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
      labelProp: "text",
      query: giveMe.aContextData({ parent: "data.value", targetProp: "query" }),
      params: giveMe.aContextData({ parent: "data.id", targetProp: "id" }),
      data: giveMe.anInlineData({
        id: "test-id",
        value: "value",
        text: "Dynamic"
      })
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