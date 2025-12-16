<template>
  <UiSpec title="Inputs">
    <Message severity="info">
      <span class="text-xl">
        ⬇️  Check if there are 4 inputs, first having `default value` ⬇️
      </span>
    </Message>
    <div
      class="flex gap-4 border-4 border-gray-500 rounded p-4"
      data-testid="component"
    >
      <Inputs
        :inputs
        @ready="onReady"
        @changed="onChanged"
      />
    </div>
    <Message severity="info">
      <span class="text-xl">
        ⬇️  Check if ready is true when all required inputs are set ⬇️
      </span>
    </Message>
    <div class="border-4 border-gray-500 rounded p-4">
      <span class="text-gray-500">ready=</span>
      <span data-testid="ready">{{ ready }}</span>
    </div>
    <Message severity="info">
      <span class="text-xl">
        ⬇️  Check if unique key changes when any input changes ⬇️
      </span>
    </Message>
    <div class="border-4 border-gray-500 rounded p-4">
      <span class="text-gray-500">unique-key=</span>
      <span data-testid="unique-key">{{ uniqueKey }}</span>
    </div>
    <Message severity="info">
      <span class="text-xl">
        ⬇️  Check if 'readyValues' same with 'uniqueKey' ⬇️
      </span>
    </Message>
    <div class="border-4 border-gray-500 rounded p-4">
      <span class="text-gray-500">onReadyValues-key=</span>
      <span data-testid="onReadyValues-key">{{ readyValues }}</span>
    </div>
  </UiSpec>
</template>
<script setup>
import { ref } from "vue";
import { Message } from "primevue";
import giveMe from "@utils/giveMe";

const ready = ref();
const uniqueKey = ref();
const readyValues = ref();

const inputs = [
  giveMe.anInput({
    name: "requiredWithDefault",
    component: giveMe.anInputText({ testId: "required-with-default" }),
    required: true,
    defaultValue: "default value"
  }),
  giveMe.anInput({
    name: "required",
    component: giveMe.anInputText({ testId: "required" }),
    required: true
  }),
  giveMe.anInput({
    name: "required-number",
    component: giveMe.anInputNumber({ testId: "required-number" }),
    required: true
  }),
  giveMe.anInput({
    name: "optional",
    component: giveMe.anInputText({ testId: "optional" })
  })
];

function onReady(value) {
  ready.value = value;
  readyValues.value = uniqueKey.value;
}

function onChanged(event) {
  uniqueKey.value = event.uniqueKey;
}
</script>
