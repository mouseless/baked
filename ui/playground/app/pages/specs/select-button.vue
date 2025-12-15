<template>
  <UiSpec
    title="Select Button"
    :variants="variants"
    :use-model="true"
    variant-class="inline"
  />
</template>
<script setup>
import { ref } from "vue";
import giveMe from "@utils/giveMe";

const variants = [
  {
    name: "Base",
    descriptor: giveMe.aSelectButton({
      data: ["OPTION_1", "OPTION_2"]
    }),
    model: ref()
  },
  {
    name: "Base w/ Localization",
    descriptor: giveMe.aSelectButton({
      localizeLabel: true,
      data: ["Spec: Option 1", "Spec: Option 2"]
    }),
    model: ref()
  },
  {
    name: "Option Label and Value",
    descriptor: giveMe.aSelectButton({
      optionLabel: "label",
      optionValue: "value",
      localizeLabel: true,
      data: [
        { label: "Spec: Label 1", value: "VALUE_1" },
        { label: "Spec: Label 2", value: "VALUE_2" }
      ]
    }),
    model: ref()
  },
  {
    name: "Allow Empty",
    descriptor: giveMe.aSelectButton({
      allowEmpty: true,
      data: ["OPTION"]
    }),
    model: ref()
  },
  {
    name: "Stateful and Not Inline",
    descriptor: giveMe.aSelectButton({
      stateful: true,
      data: ["OPTION 1", "OPTION 2"],
      inline: false
    }),
    model: ref("OPTION 1")

  },
  {
    name: "Stateful and Inline",
    descriptor: giveMe.aSelectButton({
      stateful: true,
      data: ["OPTION 1", "OPTION 2"],
      inline: true
    }),
    model: ref("OPTION 1")

  },
  {
    name: "Set Selected",
    descriptor: giveMe.aSelectButton({
      data: ["VALUE_A", "VALUE_B"],
      inline: false
    }),
    model: ref("VALUE_B")
  },
  {
    name: "Page Context",
    descriptor: giveMe.aSelectButton({
      data: ["OPTION 1", "OPTION 2"],
      action: giveMe.aPublishAction({ pageContextKey: "test:select-button" })
    }),
    model: ref(),
    pageContextKey: "test:select-button"
  },
  {
    name: "Page Context - Option Label and Value",
    descriptor: giveMe.aSelectButton({
      optionLabel: "label",
      optionValue: "value",
      data: [
        { label: "Label 1", value: "OPTION 1" },
        { label: "Label 2", value: "OPTION 2" }
      ],
      action: giveMe.aPublishAction({ pageContextKey: "test:select-button" })
    }),
    model: ref(),
    pageContextKey: "test:select-button"
  }
];
</script>
