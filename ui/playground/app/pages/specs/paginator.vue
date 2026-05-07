<template>
  <UiSpec
    title="Paginator"
    :variants
    no-loading-variant
    use-model
  >
    <Message severity="info">
      <span class="text-xl">
        ⬇️  Set values to test paginator behavior ⬇️
      </span>
    </Message>
    <div
      class="flex gap-4 border-4 border-gray-500 rounded p-4"
      data-testid="component"
    >
      <Inputs
        :inputs
      />
    </div>
  </UiSpec>
</template>
<script setup>
import { ref } from "vue";
import { Message } from "primevue";
import giveMe from "@utils/giveMe";

const variants = [
  {
    name: "Base",
    descriptor: {
      type: "Paginator",
      schema: {},
      data: {
        type: "Composite",
        parts: [
          giveMe.aContextData({ key: "page", prop: "length", targetProp: "length" }),
          giveMe.aContextData({ key: "page", prop: "take", targetProp: "take" })
        ]
      },
      reactions: {
        reload: giveMe.aTrigger({ when: "take" })
      }
    },
    model: ref()
  }
];

const inputs = [
  giveMe.anInput({
    name: "length",
    component: giveMe.anExpectedInput({
      testId: "length",
      action: giveMe.aPublishAction({
        pageContextKey: "length",
        data: giveMe.aContextData({ key: "model" }) }
      )
    })
  }),
  giveMe.anInput({
    name: "take",
    component: giveMe.anExpectedInput({
      testId: "take",
      action: giveMe.aPublishAction({
        pageContextKey: "take",
        data: giveMe.aContextData({ key: "model" }) }
      )
    })
  })
];
</script>