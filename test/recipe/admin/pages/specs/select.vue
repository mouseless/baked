<template>
  <UiSpec
    title="Select"
    :variants="variants"
    :use-model="true"
  />
</template>
<script setup>
import { ref } from "vue";
import giveMe from "~/utils/giveMe";

const variants = [
  {
    name: "Base",
    descriptor: giveMe.aSelect({
      label: "Label",
      data: ["OPTION 1", "OPTION 2"]
    }),
    model: ref()
  },
  {
    name: "Option Label and Value",
    descriptor: giveMe.aSelect({
      optionLabel: "label",
      optionValue: "value",
      data: [
        { label: "LABEL 1", value: "VALUE_1" },
        { label: "LABEL 2", value: "VALUE_2" }
      ]
    }),
    model: ref()
  },
  {
    name: "Show Clear",
    descriptor: giveMe.aSelect({
      data: ["OPTION"],
      showClear: true
    }),
    model: ref("OPTION")
  },
  {
    name: "Long Label",
    descriptor: giveMe.aSelect({
      label: "This is a long label"
    }),
    model: ref()
  },
  {
    name: "Stateful",
    descriptor: giveMe.aSelect({
      stateful: true,
      data: ["OPTION 1", "OPTION 2"]
    }),
    model: ref("OPTION 1")

  },
  {
    name: "Set Selected",
    descriptor: giveMe.aSelect({
      data: ["ValueA", "ValueB"],
      inline: false
    }),
    model: ref("ValueB")
  }
];
</script>
