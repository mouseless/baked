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
    descriptor: giveMe.aContainer({
      contents: [
        giveMe.aMissingComponent({
          path: ["page"],
          source: {
            type: "TypeModelMembers",
            path: ["TestPage"]
          }
        }),
        giveMe.aMissingComponent({
          path: ["page"],
          source: {
            type: "TypeModelMetadata",
            path: ["TestPage"]
          }
        }),
        giveMe.aMissingComponent({
          component: "ReportPage",
          path: ["page"],
          source: {
            type: "TypeModelMembers",
            path: ["TestPage"]
          }
        })
      ]
    })
  },
  {
    name: "Property",
    descriptor: giveMe.aContainer({
      contents: [
        giveMe.aMissingComponent({
          path: ["page", "data-table", "columns", "text"],
          source: {
            type: "PropertyModel",
            path: ["Record", "Text"]
          }
        }),
        giveMe.aMissingComponent({
          component: "Money",
          path: ["page", "data-table", "columns", "text"],
          source: {
            type: "PropertyModel",
            path: ["Record", "Text"]
          }
        })
      ]
    })
  },
  {
    name: "Method",
    descriptor: giveMe.aContainer({
      contents: [
        giveMe.aMissingComponent({
          path: ["page", "data-panel"],
          source: {
            type: "MethodModel",
            path: ["TestPage", "GetData"]
          },
          data: [
            { "test": "large data" },
            { "test": "large data" }
          ]
        }),
        giveMe.aMissingComponent({
          component: "SimpleForm",
          path: ["page", "data-panel"],
          source: {
            type: "MethodModel",
            path: ["TestPage", "GetData"]
          }
        })
      ]
    })
  },
  {
    name: "Parameter",
    descriptor: giveMe.aContainer({
      contents: [
        giveMe.aMissingComponent({
          path: ["page", "data-panel", "parameters"],
          source: {
            type: "ParameterModel",
            path: ["TestPage", "GetData", "panel"]
          }
        }),
        giveMe.aMissingComponent({
          component: "InputNumber",
          path: ["page", "data-panel", "parameters"],
          source: {
            type: "ParameterModel",
            path: ["TestPage", "GetData", "panel"]
          }
        })
      ]
    })
  }
];
</script>
