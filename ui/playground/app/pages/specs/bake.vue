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
          data: giveMe.aContextData({ key: "parent", prop: "data" })
        }),
        giveMe.anExpected({
          testId: "child-prop",
          data: giveMe.aContextData({ key: "parent", prop: "data.child" })
        })
      ],
      data: giveMe.anInlineData({ child: "CHILD VALUE" })
    })
  },
  {
    name: "Data Descriptor",
    descriptor: giveMe.anExpected({
      testId: "test",
      showDataParams: true,
      data: giveMe.aCompositeData([ // merges ["computed"] and ["RequiredWithDefault1", "Required1"]
        giveMe.aComputedData({ composable: "useFakeComputed", options: giveMe.anInlineData({ data: "computed" }) }), // provides "computed"
        giveMe.aContextData({ key: "parent" }),
        giveMe.anInlineData({ inline: "inline" }),
        giveMe.aRemoteData({
          path: "/route-parameters-samples/{id}",
          query: giveMe.aCompositeData([
            giveMe.anInlineData({ requiredWithDefault: "RequiredWithDefault1" }), // provides "RequiredWithDefault1"
            giveMe.anInlineData({ required: "Required1" }) // provides "Required1"
          ]),
          params: giveMe.aCompositeData([
            giveMe.anInlineData({ id: 15 }),
            giveMe.anInlineData({ id: "7b6b67bb-30b5-423e-81b4-a2a0cd59b7f9" })
          ]),
          headers: giveMe.anInlineData({ Authorization: `Bearer ${giveMe.aToken({ admin: true }).access}` })
        })
      ])
    })
  },
  {
    name: "Model",
    descriptor: giveMe.anInputText(),
    model: ref("Model Data")
  },
  {
    name: "Action",
    descriptor: giveMe.aButton({
      action: {
        type: "Composite",
        parts: [
          {
            type: "Local",
            composable: "useShowMessage",
            options: giveMe.anInlineData({ message: "Execute Action" })
          },
          {
            type: "Local",
            composable: "useDelay",
            options: giveMe.anInlineData({ time: 100 })
          },
          {
            type: "Remote",
            path: "/rich-transient-with-datas/{id}/method",
            method: "POST",
            headers: giveMe.anInlineData({ Authorization: "token-admin-ui" }),
            query: giveMe.theQueryData(),
            params: giveMe.anInlineData({ id: 12 }),
            body: giveMe.anInlineData({ text: "text" }),
            postAction: {
              type: "Local",
              composable: "useShowMessage",
              options: giveMe.anInlineData({ message: "Execute Post Action" })
            }
          }
        ]
      },
      label: "Spec: Button",
      icon: "pi pi-play-circle"
    })
  },
  {
    name: "Reaction",
    descriptor: giveMe.aContainer({
      contents: [
        giveMe.aButton({
          action: {
            type: "Emit",
            event: "clicked"
          }
        }),
        giveMe.anInputText({ testId: "input", action: { type: "PageContext", key: "input" } }),
        giveMe.aText({
          data: giveMe.aRemoteData({
            path: "/localization-samples/locale-string",
            headers: giveMe.anInlineData({ Authorization: "token-admin-ui" })
          }),
          reactions: {
            reload: giveMe.anOnTrigger({ on: "clicked" }),
            show: giveMe.aWhenTrigger({
              when: "input",
              constraint: { type: "Is", isNot: "hide" } // TODO more cases
            })
          }
        })
      ]
    })
  }
];
</script>
