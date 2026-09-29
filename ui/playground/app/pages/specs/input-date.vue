<template>
  <UiSpec
    :variants
    use-model
  />
</template>
<script setup>
import { computed, ref } from "vue";
import giveMe from "@utils/giveMe";

const variants = [
  {
    name: "Base",
    descriptor: giveMe.anInputDate({
      label: "Spec: Label"
    }),
    model: ref("2026-01-15"),
    validation: computed(() => ({
      severity: "error"
    }))
  },
  {
    name: "Use Picker",
    descriptor: giveMe.anInputDate({
      label: "Spec: Label",
      usePicker: true,
      format: "dd/mm/yy"
    }),
    model: ref("2026-01-15"),
    validation: computed(() => ({
      severity: "error"
    }))
  },
  {
    name: "Format",
    descriptor: giveMe.anInputDate({
      label: "Spec: Label",
      format: "dd.MM.yyyy"
    }),
    model: ref("2026-01-15"),
    validation: computed(() => ({
      severity: "error"
    }))
  }
];
</script>