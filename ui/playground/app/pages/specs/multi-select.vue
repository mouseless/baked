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
    descriptor: giveMe.aMultiSelect({
      label: "Label",
      data: ["OPTION_1", "OPTION_2"]
    }),
    model: ref()
  },
  {
    name: "Base w/ Localization",
    descriptor: giveMe.aMultiSelect({
      label: "Label",
      localizeOptionLabels: true,
      data: ["Spec: Option 1", "Spec: Option 2"]
    }),
    model: ref()
  },
  {
    name: "Option Label and Value",
    descriptor: giveMe.aMultiSelect({
      optionLabel: "label",
      optionValue: "value",
      data: [
        { label: "LABEL_1", value: "VALUE_1" },
        { label: "LABEL_2", value: "VALUE_2" }
      ]
    }),
    model: ref()
  },
  {
    name: "Option Label and Value with Localization",
    descriptor: giveMe.aMultiSelect({
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
    name: "Show Clear",
    descriptor: giveMe.aMultiSelect({
      data: ["OPTION"],
      showClear: true
    }),
    model: ref(["OPTION"])
  },
  {
    name: "Max Selected Labels",
    descriptor: giveMe.aMultiSelect({
      data: ["OPTION_1", "OPTION_2", "OPTION_3", "OPTION_4"]
    }),
    model: ref()
  },
  {
    name: "Show Toggle All",
    descriptor: giveMe.aMultiSelect({
      showToggleAll: true,
      data: ["OPTION_1", "OPTION_2"]
    }),
    model: ref()
  },
  {
    name: "Stateful and Not Inline",
    descriptor: giveMe.aMultiSelect({
      stateful: true,
      data: ["OPTION 1", "OPTION 2"],
      inline: false
    }),
    model: ref(["OPTION 1"])
  },
  {
    name: "Stateful and Inline",
    descriptor: giveMe.aMultiSelect({
      stateful: true,
      data: ["OPTION 1", "OPTION 2"],
      inline: true
    }),
    model: ref(["OPTION 1"])
  },
  {
    name: "Set Selected",
    descriptor: giveMe.aMultiSelect({
      data: ["ValueA", "ValueB"],
      inline: false
    }),
    model: ref(["ValueA", "ValueB"])
  },
  {
    name: "Page Context",
    descriptor: giveMe.aMultiSelect({
      data: ["ValueA", "ValueB"],
      inline: false,
      action: giveMe.aPublishAction({ pageContextKey: "test:multi-select" })
    }),
    model: ref(),
    pageContextKey: "test:multi-select"
  }
];
</script>
