<template>
  <UiSpec :variants />
</template>
<script setup>
import giveMe from "@utils/giveMe";

const variants = [
  {
    name: "Base",
    descriptor: giveMe.anAvatar({
      data: giveMe.anInlineData("TS")
    })
  },
  {
    name: "Large",
    descriptor: giveMe.anAvatar({
      icon: "pi pi-box",
      size: "large"
    })
  },
  {
    name: "XLarge",
    descriptor: giveMe.anAvatar({
      icon: "pi pi-box",
      size: "xlarge"
    })
  }
];
</script>