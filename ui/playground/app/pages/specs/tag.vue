<template>
  <UiSpec :variants />
</template>
<script setup>
import giveMe from "@utils/giveMe";

const variants = [
  {
    name: "Base",
    descriptor: giveMe.aTag({
      value: "success",
      localizationPrefix: "EntityStatus",
      localizeText: true

    })
  },
  {
    name: "Array",
    descriptor: giveMe.aTag({
      data: giveMe.anInlineData(["item 1", "item 2"])
    })
  }
];
</script>