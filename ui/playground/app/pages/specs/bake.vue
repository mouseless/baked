<template>
  <UiSpec
    title="Bake"
    :variants="variants"
  />
</template>
<script setup>
import giveMe from "@utils/giveMe";

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
        type: "Composite", // merges ["computed"] and ["RequiredWithDefault1", "Required1"]
        parts: [
          { type: "Computed", composable: "useFakeComputed", args: ["computed"] }, // provides "computed"
          { type: "Injected", key: "ParentData" },
          { type: "Inline", value: { inline: "inline" } },
          {
            type: "Remote",
            path: "/report-page-sample/wide",
            query: {
              type: "Composite", // merges ["RequiredWithDefault1"] and ["Required1"]
              parts: [
                { type: "Inline", value: { requiredWithDefault: "RequiredWithDefault1" } }, // provides "RequiredWithDefault1"
                { type: "Inline", value: { required: "Required1" } } // provides "Required1"
              ]
            },
            headers: {
              type: "Inline",
              value: { "Authorization": `Bearer ${giveMe.aToken({ admin: true }).access}` }
            }
          }
        ]
      }
    })
  }
];
</script>
