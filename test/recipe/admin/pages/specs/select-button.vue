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
import giveMe from "~/utils/giveMe";

const variants = [
  {
    name: "Base",
    descriptor: giveMe.aSelectButton({
      localizeLabel: true,
      data: ["OPTION_1", "OPTION_2"]
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
        { label: "Spec.Label_1", value: "VALUE_1" },
        { label: "Spec.Label_2", value: "VALUE_2" }
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
    name: "Stateful",
    descriptor: giveMe.aSelectButton({
      stateful: true,
      data: ["OPTION 1", "OPTION 2"]
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
  }
];
</script>
