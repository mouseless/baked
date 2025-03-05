<template>
  <ComponentSpec :variants="variants" />
</template>
<script setup>
import { giveMe } from "#imports";

const variants = [
  {
    name: "Basic",
    descriptor: giveMe.aDetail({
      title: "TITLE TEXT",
      header: giveMe.anExpected({ testId: "header", value: "HEADER TEXT" }),
      props: [
        giveMe.aDetailProp({ keyAndTestId: "prop1", title: "PROP 1" }),
        giveMe.aDetailProp({ keyAndTestId: "prop2", title: "PROP 2" })
      ],
      data: {
        "prop1": "PROP1 VALUE",
        "prop2": "PROP2 VALUE"
      }
    })
  },
  {
    name: "Null",
    descriptor: giveMe.aDetail({
      title: null,
      header: null,
      props: null,
      data: null
    })
  },
  {
    name: "Long Property",
    descriptor: giveMe.aDetail({
      props: [
        giveMe.aDetailProp({
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
