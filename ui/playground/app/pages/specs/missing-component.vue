<template>
  <UiSpec
    title="Missing Component"
    :variants="variants"
    :vertical="false"
  />
</template>
<script setup>
import giveMe from "@utils/giveMe";

const variants = [
  {
    name: "Base",
    descriptor: giveMe.aMissingComponent({
      path: ["test", "path"],
      data: "Sample Data"
    })
  },
  {
    name: "Type",
    descriptor: giveMe.aMissingComponent({
      path: ["page"],
      source: {
        type: "TypeModelMembers",
        path: ["TestPage"]
      }
    })
  },
  {
    name: "Property",
    descriptor: giveMe.aMissingComponent({
      path: ["page", "data-table", "columns", "text"],
      source: {
        type: "PropertyModel",
        path: ["Record", "Text"]
      }
    })
  },
  {
    name: "Method",
    descriptor: giveMe.aMissingComponent({
      path: ["page", "data-panel"],
      source: {
        type: "MethodModel",
        path: ["TestPage", "GetData"]
      },
      data: [
        { "test": "large data" },
        { "test": "large data" }
      ]
    })
  },
  {
    name: "Parameter",
    descriptor: giveMe.aMissingComponent({
      path: ["page", "data-panel", "parameters"],
      source: {
        type: "ParameterModel",
        path: ["TestPage", "GetData", "panel"]
      }
    })
  },
  {
    name: "TypeMetadata",
    descriptor: giveMe.aMissingComponent({
      path: ["page"],
      source: {
        type: "TypeModelMetadata",
        path: ["TestPage"]
      }
    })
  }
];
</script>
