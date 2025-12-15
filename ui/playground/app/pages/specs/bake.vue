<template>
  <UiSpec
    title="Bake"
    :variants="variants"
    no-loading-variant
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
      action: giveMe.aCompositeAction([
        giveMe.aLocalAction({ showMessage: "Execute Action" }),
        giveMe.aLocalAction({ delay: 100 }),
        giveMe.aRemoteAction({
          path: "/rich-transient-with-datas/{id}/method",
          method: "POST",
          headers: giveMe.anInlineData({ Authorization: "token-admin-ui" }),
          query: giveMe.theQueryData(),
          params: giveMe.anInlineData({ id: 12 }),
          body: giveMe.anInlineData({ text: "text" }),
          postAction: giveMe.aLocalAction({
            composable: "useShowMessage",
            options: giveMe.aContextData({ key: "response", targetProp: "message" })
          })
        })
      ]),
      label: "Spec: Button",
      icon: "pi pi-play-circle"
    })
  },
  {
    name: "Reaction",
    descriptor: giveMe.aContainer({
      contents: [
        giveMe.aButton({
          label: "Spec: Reload",
          action: giveMe.aPublishAction({ event: "clicked" })
        }),
        giveMe.anInputText({
          testId: "input",
          action: giveMe.aCompositeAction([
            giveMe.aPublishAction({ event: "input-changed" }),
            giveMe.aPublishAction({ pageContextKey: "input" })
          ])
        }),
        giveMe.anExpected({
          testId: "output",
          data: giveMe.aRemoteData({
            path: "/method-samples/async?ms=10",
            headers: giveMe.anInlineData({ Authorization: "token-admin-ui" })
          }),
          reactions: {
            reload: giveMe.aTrigger({
              parts: [
                giveMe.aTrigger({ on: "clicked" }),
                giveMe.aTrigger({ on: "input-changed", constraint: giveMe.aConstraint({ is: "event" }) }),
                giveMe.aTrigger({ when: "input", constraint: giveMe.aConstraint({ is: "page-context" }) }),
                giveMe.aTrigger({
                  when: "input",
                  constraint: giveMe.aConstraint({
                    composable: "useFakeValidator",
                    options: giveMe.anInlineData({ expected: "validate" })
                  })
                })
              ]
            }),
            show: giveMe.aTrigger({ when: "input", constraint: giveMe.aConstraint({ isNot: "hide" }) })
          }
        })
      ]
    })
  }
];
</script>
