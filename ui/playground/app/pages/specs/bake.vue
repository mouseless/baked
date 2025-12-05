<template>
  <UiSpec
    title="Bake"
    :variants="variants"
  />
</template>
<script setup>
import { ref } from "vue";
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
          data: { type: "Context", key: "ParentData" }
        }),
        giveMe.anExpected({
          testId: "child-prop",
          data: { type: "Context", key: "ParentData", prop: "child" }
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
          { type: "Computed", composable: "useFakeComputed", options: { type: "Inline", value: { data: "computed" } } }, // provides "computed"
          { type: "Context", key: "ParentData" },
          { type: "Inline", value: { inline: "inline" } },
          {
            type: "Remote",
            path: "/route-parameters-samples/{id}",
            query: {
              type: "Composite", // merges ["RequiredWithDefault1"] and ["Required1"]
              parts: [
                { type: "Inline", value: { requiredWithDefault: "RequiredWithDefault1" } }, // provides "RequiredWithDefault1"
                { type: "Inline", value: { required: "Required1" } } // provides "Required1"
              ]
            },
            params: {
              type: "Composite",
              parts: [
                { type: "Inline", value: { id: 15 } },
                { type: "Inline", value: { id: "7b6b67bb-30b5-423e-81b4-a2a0cd59b7f9" } }
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
  },
  {
    name: "Model",
    descriptor: giveMe.aContainer({
      contents:[
        giveMe.anInputText()
      ]
    }),
    model: ref("Model Data")
  },
  {
    name: "Action",
    descriptor: giveMe.aContainer({
      contents:[
        giveMe.aButton({
          action: {
            type: "Composite",
            parts: [
              {
                type: "Local",
                composable: "useShowMessage",
                options: {
                  type: "Inline",
                  value: { message: "Execute Action" }
                }
              },
              {
                type: "Local",
                composable: "useDelay",
                options: {
                  type: "Inline",
                  value: { time: 100 }
                }
              },
              {
                type: "Remote",
                path: "/rich-transient-with-datas/{id}/method",
                method: "POST",
                headers: {
                  type: "Inline",
                  value: {
                    Authorization: "token-admin-ui"
                  }
                },
                query: {
                  type: "Computed",
                  composable: "useNuxtRoute",
                  options: {
                    type: "Inline",
                    value:{ property: "query" }
                  }
                },
                params: {
                  type: "Inline",
                  value: {
                    id: 12
                  }
                },
                body: {
                  type: "Inline",
                  value: {
                    text: "text"
                  }
                },
                postAction: {
                  type: "Local",
                  composable: "useShowMessage",
                  options: {
                    type: "Inline",
                    value: { message: "Execute Post Action" }
                  }
                }
              }
            ]
          },
          label: "Spec: Button",
          icon: "pi pi-play-circle"
        })
      ]
    })
  }
];
</script>
