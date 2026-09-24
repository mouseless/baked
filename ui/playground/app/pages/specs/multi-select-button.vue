<template>
  <UiSpec
    :variants
    use-model
  />
</template>
<script setup>
import { ref } from "vue";
import giveMe from "@utils/giveMe";

const variants = [
  {
    name: "Base",
    descriptor: giveMe.aMultiSelectButton({
      label: "Label",
      data: ["OPTION_1", "OPTION_2"]
    }),
    model: ref()
  },
  {
    name: "Base w/ Localization",
    descriptor: giveMe.aMultiSelectButton({
      label: "Label",
      localizeOptionLabels: true,
      data: ["Spec: Option 1", "Spec: Option 2"]
    }),
    model: ref()
  },
  {
    name: "Option Label and Value",
    descriptor: giveMe.aMultiSelectButton({
      optionLabel: "label",
      optionValue: "value",
      localizeOptionLabels: true,
      data: [
        { label: "Spec: Label 1", value: "VALUE_1" },
        { label: "Spec: Label 2", value: "VALUE_2" }
      ]
    }),
    model: ref()
  },
  {
    name: "Allow Empty",
    descriptor: giveMe.aMultiSelectButton({
      allowEmpty: true,
      data: ["OPTION"]
    }),
    model: ref()
  },
  {
    name: "Stateful and Not Inline",
    descriptor: giveMe.aMultiSelectButton({
      stateful: true,
      data: ["OPTION 1", "OPTION 2"],
      inline: false
    }),
    model: ref()
  },
  {
    name: "Stateful and Inline",
    descriptor: giveMe.aMultiSelectButton({
      stateful: true,
      data: ["OPTION 1", "OPTION 2"],
      inline: true
    }),
    model: ref()
  },
  {
    name: "Set Selected",
    descriptor: giveMe.aMultiSelectButton({
      data: ["VALUE_A", "VALUE_B"],
      inline: false
    }),
    model: ref([ "VALUE_B" ])
  },
  {
    name: "Page Context",
    descriptor: giveMe.aMultiSelectButton({
      data: ["OPTION 1", "OPTION 2"],
      action: giveMe.aPublishAction({ pageContextKey: "test:multi-select-button" })
    }),
    model: ref(),
    pageContextKey: "test:multi-select-button"
  },
  {
    name: "Page Context - Option Label and Value",
    descriptor: giveMe.aMultiSelectButton({
      optionLabel: "label",
      optionValue: "value",
      data: [
        { label: "Label 1", value: "OPTION 1" },
        { label: "Label 2", value: "OPTION 2" }
      ],
      action: giveMe.aPublishAction({ pageContextKey: "test:multi-select-button" })
    }),
    model: ref(),
    pageContextKey: "test:multi-select-button"
  },
  {
    name: "Target Prop",
    descriptor: giveMe.aMultiSelectButton({
      targetProp: "id",
      optionLabel: "label",
      optionValue: "code",
      data: [
        { code: "VALUE_A", label: "Label A" },
        { code: "VALUE_B", label: "Label B" }
      ],
      stateful: true,
      allowEmpty: true
    }),
    model: ref()
  }
];
</script>
