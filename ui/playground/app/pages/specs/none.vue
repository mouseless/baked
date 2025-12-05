<template>
  <UiSpec
    title="None"
    :variants="variants"
    :vertical="false"
  />
</template>
<script setup>
import giveMe from "@utils/giveMe";

const variants = [
  {
    name: "Base",
    descriptor: giveMe.aNone({
      path: ["test", "path"],
      data: "Sample Data"
    })
  },
  {
    name: "Type",
    descriptor: giveMe.aNone({
      path: ["page"],
      source: {
        type: "TypeModelMembers",
        path: ["TestPage"]
      }
    })
  },
  {
    name: "Property",
    descriptor: giveMe.aNone({
      path: ["page", "data-table", "columns", "text"],
      source: {
        type: "PropertyModel",
        path: ["Record", "Text"]
      }
    })
  },
  {
    name: "Method",
    descriptor: giveMe.aNone({
      path: ["page", "data-panel"],
      source: {
        type: "MethodModel",
        path: ["TestPage", "GetData"]
      }
    })
  },
  {
    name: "Parameter",
    descriptor: giveMe.aNone({
      path: ["page", "data-panel", "parameters"],
      source: {
        type: "ParameterModel",
        path: ["TestPage", "GetData", "panel"]
      }
    })
  },
  {
    name: "TypeMetadata",
    descriptor: giveMe.aNone({
      path: ["page"],
      source: {
        type: "TypeModelMetadata",
        path: ["TestPage"]
      }
    })
  }
];
</script>
