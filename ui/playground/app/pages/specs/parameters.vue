<template>
  <UiSpec title="Parameters">
    <Message severity="info">
      <span class="text-xl">
        ⬇️  Check if there are 3 parameters, first having `default value` ⬇️
      </span>
    </Message>
    <div
      class="border-4 border-gray-500 rounded p-4 space-x-4"
      data-testid="component"
    >
      <Parameters
        :parameters
        @ready="onReady"
        @changed="onChanged"
      />
    </div>
    <Message severity="info">
      <span class="text-xl">
        ⬇️  Check if ready is true when all required parameters are set ⬇️
      </span>
    </Message>
    <div class="border-4 border-gray-500 rounded p-4">
      <span class="text-gray-500">ready=</span>
      <span data-testid="ready">{{ ready }}</span>
    </div>
    <Message severity="info">
      <span class="text-xl">
        ⬇️  Check if unique key changes when any parameter changes ⬇️
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

const parameters = [
  giveMe.aParameter({
    name: "requiredWithDefault",
    component: giveMe.anInput({ testId: "required-with-default" }),
    required: true,
    defaultValue: "default value"
  }),
  giveMe.aParameter({
    name: "required",
    component: giveMe.anInput({ testId: "required" }),
    required: true
  }),
  giveMe.aParameter({
    name: "optional",
    component: giveMe.anInput({ testId: "optional" })
  }),
  giveMe.aParameter({
    name: "num-required",
    component: giveMe.anInputNumber({ testId: "num-required" }),
    required: true
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
