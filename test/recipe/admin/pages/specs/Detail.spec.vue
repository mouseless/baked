<template>
  <Baked.ComponentSpec :variants="variants" />
</template>
<script setup>
const giveMe = {
  aDetail(descriptor = { })
  {
    descriptor.title = descriptor.title === undefined ? "test title" : descriptor.title;

    return descriptor;
  }
};

const variants = [
  {
    name: "Basic",
    descriptor: {
      "type": "Detail",
      "schema": giveMe.aDetail({
        title: "TITLE TEXT",
        "header": {
          "type": "Expected",
          "schema": "header",
          "data": "HEADER TEXT"
        },
        "props": [
          {
            "key": "prop1",
            "title": "PROP1",
            "component": { "type": "Expected", "schema": "prop1" }
          },
          {
            "key": "prop2",
            "title": "PROP2",
            "component": { "type": "Expected", "schema": "prop2" }
          }
        ],
        "data": {
          "prop1": "PROP1 VALUE",
          "prop2": "PROP2 VALUE"
        }
      })
    }
  },
  {
    name: "Null",
    descriptor: {
      "type": "Detail",
      "schema": giveMe.aDetail({
        title: null
      })
    }
  },
  {
    name: "Long Property",
    descriptor: {
      "type": "Detail",
      "schema": {
        "props": [
          {
            "key": "longProp",
            "title": "A VERY LONG PROP NAME",
            "component": { "type": "Expected", "schema": "longProp" }
          }
        ]
      },
      "data": {
        "longProp": "LONG TEXT LONG TEXT LONG TEXT LONG TEXT LONG TEXT LONG TEXT LONG TEXT LONG TEXT"
      }
    }
  }
];

</script>
