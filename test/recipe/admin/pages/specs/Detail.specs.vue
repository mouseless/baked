<template>
  <Menubar :model="variantLinks" />
  <div
    v-for="variant in variants"
    :key="variant.name"
    class="my-4"
  >
    <h2 :id="variant.name" class="text-xl font-bold mb-2">{{variant.name}}</h2>
    <Baked.Component :descriptor="variant.descriptor" />
  </div>
</template>
<script setup>
const variants = [
  {
    name: "Basic",
    descriptor: {
      "type": "Detail",
      "schema": {
        "title": "TITLE TEXT",
        "header": {
          "type": "String",
          "data": "HEADER TEXT"
        },
        "props": [
          {
            "key": "prop1",
            "title": "PROP1",
            "component": { "type": "String" }
          },
          {
            "key": "prop2",
            "title": "PROP2",
            "component": { "type": "String" }
          }
        ]
      },
      "data": {
        "prop1": "PROP1 VALUE",
        "prop2": "PROP2 VALUE"
      }
    }
  },
  {
    name: "Null",
    descriptor: {
      "type": "Detail",
      "schema": { }
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
            "component": { "type": "String" }
          }
        ]
      },
      "data": {
        "longProp": "LONG TEXT LONG TEXT LONG TEXT LONG TEXT LONG TEXT LONG TEXT LONG TEXT LONG TEXT"
      }
    }
  }
];

const variantLinks = computed(() => variants.map(v => ({ label: v.name, url: `#${v.name}`})));
</script>
