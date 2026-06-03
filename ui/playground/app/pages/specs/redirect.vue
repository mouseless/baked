<template>
  <UiSpec
    :variants
    no-loading-variant
  />
</template>
<script setup>
import giveMe from "@utils/giveMe";

const variants = [
  {
    name: "Base",
    descriptor: giveMe.aButton({
      action: giveMe.aLocalAction({
        redirect: "/page/with/route/pageWithRoute"
      })
    })
  },
  {
    name: "Conditional",
    descriptor: giveMe.anExpectedInput({
      action: giveMe.aLocalAction({
        redirect: "/page/with/route/pageWithRoute",
        options: giveMe.aCompositeData([
          giveMe.anInlineData({ expected: "redirect" }),
          giveMe.aContextData({ key: "model", targetProp: "actual" })
        ])
      })
    })
  },
  {
    name: "Dynamic",
    descriptor: giveMe.aButton({
      action: giveMe.aLocalAction({
        redirect: "/page/with/route/[id]",
        options: giveMe.aCompositeData([
          giveMe.anInlineData({ route: "/page/with/route/[id]" }),
          giveMe.anInlineData({ id: "42" })
        ])
      })
    })
  }
];
</script>