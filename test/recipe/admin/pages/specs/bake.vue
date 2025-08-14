<template>
  <UiSpec
    title="Bake"
    :variants="variants"
  />
</template>
<script setup>
import giveMe from "~/utils/giveMe";

const variants = [
  {
    name: "Base",
    descriptor: giveMe.anExpected({ value: "TEST" })
  },
  {
    name: "Parent Data",
    descriptor: giveMe.aContainer({
      contents: [
        giveMe.anExpected({
          testId: "child-root",
          data: { type: "Injected", key: "ParentData" }
        }),
        giveMe.anExpected({
          testId: "child-prop",
          data: { type: "Injected", key: "ParentData", prop: "child" }
        })
      ],
      data: { type: "Inline", value: { child: "CHILD VALUE" } }
    })
  },
  {
    name: "Data Descriptor",
    descriptor: giveMe.anExpected({
      testId: "test",
      showDataParams: true,
      data: {
        type: "Composite",
        parts: [
          { type: "Inline", value: { inline: "inline" } },
          { type: "Computed", composable: "useFakeComputed", args: ["computed"] },
          { type: "Injected", key: "ParentData" },
          {
            type: "Remote",
            path: "/report/wide",
            query: {
              type: "Inline",
              value: { requiredWithDefault: "remote-1", required: "remote-2" }
            },
            headers: {
              type: "Inline",
              value: { "Authorization": `Bearer ${giveMe.aToken({ admin: true }).access}`}
            }
          }
        ]
      }
    })
  }
];
</script>
