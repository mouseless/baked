<template>
  <UiSpec
    title="Conditional"
    :variants="variants"
    :no-loading-variant="true"
  />
</template>
<script setup>
import giveMe from "@utils/giveMe";

const variants = [
  {
    name: "Base",
    descriptor: giveMe.aConditional({
      conditions: [
        giveMe.aConditionalCondition({ prop: "type", value: "type-1", component: giveMe.anExpected({ testId: "component-1", data: { type: "Context", key: "parent", prop: "data.type" } }) }),
        giveMe.aConditionalCondition({ prop: "type", value: "type-2", component: giveMe.anExpected({ testId: "component-2", data: { type: "Context", key: "parent", prop: "data.type" } }) })
      ],
      data: {
        type: "Inline",
        value: { type: "type-2" }
      }
    })
  },
  {
    name: "Fallback",
    descriptor: giveMe.aConditional({
      conditions: [
        giveMe.aConditionalCondition({ prop: "type", value: "type-1", component: giveMe.anExpected({ testId: "component-1", data: { type: "Context", key: "parent", prop: "data.type" } }) }),
        giveMe.aConditionalCondition({ prop: "type", value: "type-2", component: giveMe.anExpected({ testId: "component-2", data: { type: "Context", key: "parent", prop: "data.type" } }) })
      ],
      fallback: giveMe.anExpected({ testId: "component-3", data: { type: "Context", key: "parent", prop: "data.type" } }),
      data: {
        type: "Inline",
        value: { type: "type-3" }
      }
    })
  }
];
</script>