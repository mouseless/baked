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
        path: ["TestPage", "GetData"]
      }
    })
  },
  {
    name: "Property",
    descriptor: giveMe.aNone({
      path: ["page", "data-table", "columns", "test"],
      source: {
        type: "PropertyModel",
        path: ["TestPage", "GetData"]
      }
    })
  },
  {
    name: "Method",
    descriptor: giveMe.aNone({
      path: ["page", "data-panel", "data-table"],
      source: {
        type: "MethodModel",
        path: ["TestPage", "GetData"]
      }
    })
  },
  {
    name: "Parameter",
    descriptor: giveMe.aNone({
      path: ["page", "data-panel", "parameters", "test"],
      source: {
        type: "ParameterModel",
        path: ["TestPage", "GetData", "panel"]
      }
    })
  }
];
</script>
