<template>
  <UiSpec>
    <Message
      :schema="{ size: 'large' }"
      data="⬇️  Check if default values are set ⬇️"
    />
    <div
      class="flex gap-4 border-4 border-gray-500 rounded p-4"
      data-testid="component"
    >
      <Inputs
        :inputs
        form-mode
        @ready="onReady"
        @changed="onChanged"
      />
    </div>
    <Message
      :schema="{ size: 'large' }"
      data="⬇️  Check if ready stays false when input with default is cleared ⬇️"
    />
    <div class="border-4 border-gray-500 rounded p-4">
      <span class="text-gray-500">ready=</span>
      <span data-testid="ready">{{ ready }}</span>
    </div>
    <Message
      :schema="{ size: 'large' }"
      data="⬇️  Check if field remove from values when input default is cleared ⬇️"
    />
    <div class="border-4 border-gray-500 rounded p-4">
      <span class="text-gray-500">values=</span>
      <span data-testid="values">{{ values }}</span>
    </div>
  </UiSpec>
</template>
<script setup>
import { ref } from "vue";
import { Message } from "#components";
import giveMe from "@utils/giveMe";

const ready = ref();
const values = ref();

const inputs = [
  giveMe.anInput({
    name: "required-with-default",
    component: giveMe.anExpectedInput({ testId: "required-with-default" }),
    required: true,
    defaultValue: "default"
  })
];

function onReady(value) {
  ready.value = value;
}

function onChanged(event) {
  values.value = event.values;
}
</script>
