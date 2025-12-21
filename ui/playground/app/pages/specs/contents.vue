<template>
  <UiSpec
    title="Inputs"
    full-page
  >
    <div
      v-for="variant in variants"
      :key="variant.name"
    >
      <h2 class="text-lg font-semibold mt-2">{{ variant.name }}</h2>
      <Divider />
      <div
        class="w-full flex justify-center"
        :data-testid="variant.name"
      >
        <Contents v-bind="variant.schema" />
      </div>
    </div>
  </UiSpec>
</template>
<script setup>
import { Divider } from "primevue";
import giveMe from "@utils/giveMe";

const variants = [
  {
    name: "Base",
    schema: {
      contents: [
        giveMe.aContent({
          component: giveMe.anExpected({ testId: "content-1", value: "CONTENT_1" }),
          narrow: true
        }),
        giveMe.aContent({
          component: giveMe.anExpected({ testId: "content-2", value: "CONTENT_2" }),
          narrow: true
        }),
        giveMe.aContent({
          component: giveMe.anExpected({ testId: "content-3", value: "CONTENT_3" })
        })
      ]
    }
  },
  {
    name: "Full Screen",
    schema: {
      contents: [giveMe.aContent()],
      fullScreen: true
    }
  }
];
</script>
