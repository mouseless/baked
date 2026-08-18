<template>
  <UiSpec>
    <Message
      :schema="{ size: 'large' }"
      data="⬇️  Check if ready is emitted true when ALL inputs are optional and untouched ⬇️"
    />
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
    <Message
      :schema="{ size: 'large' }"
      data="⬇️  ready should be true immediately, without touching any input ⬇️"
    />
    <div class="border-4 border-gray-500 rounded p-4">
      <span class="text-gray-500">ready=</span>
      <span data-testid="ready">{{ ready }}</span>
    </div>
    <Message
      :schema="{ size: 'large' }"
      data="⬇️  changed should also be emitted, uniqueKey may be empty string ⬇️"
    />
    <div class="border-4 border-gray-500 rounded p-4">
      <span class="text-gray-500">unique-key=</span>
      <span data-testid="unique-key">{{ uniqueKey }}</span>
    </div>
  </UiSpec>
</template>
<script setup>
import { ref } from "vue";
import { Message } from "#components";
import giveMe from "@utils/giveMe";

const ready = ref();
const uniqueKey = ref();

const inputs = [
  giveMe.anInput({
    name: "optional-with-null-default",
    component: giveMe.anExpectedInput({ testId: "optional-with-null-default" }),
    default_: giveMe.anInlineData()
  })
];

function onReady(value) {
  ready.value = value;
}

function onChanged(event) {
  uniqueKey.value = event.uniqueKey;
}
</script>