<template>
  <UiSpec :variants />
</template>
<script setup>
import giveMe from "@utils/giveMe";

const variants = [
  {
    name: "Base",
    descriptor: giveMe.aDate({ value: "2026-01-15", format: null })
  },
  {
    name: "Format Positioning",
    descriptor: giveMe.aContainer({
      contents: [
        giveMe.aDate({ value: "2026-01-15", format: "MM-dd-yyyy" }),
        giveMe.aDate({ value: "2026-01-15", format: "dd-MM-yyyy" }),
        giveMe.aDate({ value: "2026-01-15", format: "yyyy-MM-dd" })
      ]
    })
  },
  {
    name: "Format divide char",
    descriptor: giveMe.aContainer({
      contents: [
        giveMe.aDate({ value: "2026-01-15", format: "dd.MM.yyyy" }),
        giveMe.aDate({ value: "2026-01-15", format: "dd/MM/yyyy" })
      ]
    })
  },
  {
    name: "Time Formatting",
    descriptor: giveMe.aContainer({
      contents: [
        giveMe.aDate({ value: "2026-01-15T14:30:45", format: "HH:mm:ss" }),
        giveMe.aDate({ value: "2026-01-15T14:30:45", format: "HH:mm" }),
        giveMe.aDate({ value: "2026-01-15T14:30:45", format: "yyyy-MM-dd HH:mm:ss" })
      ]
    })
  },
  {
    name: "Prop",
    descriptor: giveMe.aDate({
      format: null,
      prop: "date",
      data: giveMe.anInlineData({
        id: 1,
        date: "2026-01-15"
      })
    })
  }
];
</script>