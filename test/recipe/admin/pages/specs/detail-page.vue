<template>
  <UiSpec
    title="Detail Page"
    :variants="variants"
    :no-loading-variant="true"
  />
</template>
<script setup>
import giveMe from "~/utils/giveMe";

const variants = [
  {
    name: "Base",
    descriptor: giveMe.aDetailPage({
      header: giveMe.anExpected({ testId: "header", value: "HEADER TEXT" }),
      props: [
        giveMe.aDetailPageProp({ keyAndTestId: "prop1", title: "PROP 1" }),
        giveMe.aDetailPageProp({ keyAndTestId: "prop2", title: "PROP 2" })
      ],
      data: {
        "prop1": "PROP1 VALUE",
        "prop2": "PROP2 VALUE"
      }
    })
  },
  {
    name: "Null",
    descriptor: giveMe.aDetailPage({
      header: null,
      props: null,
      data: null
    })
  },
  {
    name: "Long Property",
    descriptor: giveMe.aDetailPage({
      props: [
        giveMe.aDetailPageProp({
          keyAndTestId: "longProp",
          title: "A VERY LONG PROP NAME"
        })
      ]
      ,
      data: {
        "longProp": `
          LONG TEXT LONG TEXT LONG TEXT LONG TEXT
          LONG TEXT LONG TEXT LONG TEXT LONG TEXT
        `
      }
    })
  }
];
</script>
