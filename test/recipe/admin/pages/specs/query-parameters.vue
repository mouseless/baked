<template>
  <UiSpec title="Query Parameters">
    <Message severity="info">
      <span class="text-xl">
        ⬆️  Check if values sync with query string above ⬆️
      </span>
    </Message>
    <div
      class="border-4 border-gray-500 rounded p-4 space-x-4"
      data-testid="component"
    >
      <QueryParameters
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
        ⬇️  Click and check if resets all params except required with default ⬆️
      </span>
    </Message>
    <div class="border-4 border-gray-500 rounded p-4">
      <Button
        as="router-link"
        label="RESET"
        to="/specs/query-parameters"
        data-testid="reset"
      />
    </div>
  </UiSpec>
</template>
<script setup>
import { ref } from "vue";
import { Button, Message } from "primevue";
import giveMe from "~/utils/giveMe";

const ready = ref();
const uniqueKey = ref();

const parameters = [
  giveMe.aParameter({
    name: "requiredWithDefault",
    component: giveMe.anInput({ testId: "required-with-default" }),
    required: true,
    defaultValue: "default value"
  }),
  giveMe.aParameter({
    name: "requiredWithSelfManagedDefault",
    required: true,
    selfManagedDefault: true,
    component: giveMe.anInput({ testId: "required-with-self-managed-default", defaultValue: "default" })
  }),
  giveMe.aParameter({
    name: "required",
    component: giveMe.anInput({ testId: "required" }),
    required: true
  }),
  giveMe.aParameter({
    name: "optional",
    component: giveMe.anInput({ testId: "optional" })
  })
];

function onReady(value) {
  ready.value = value;
}

function onChanged(value) {
  uniqueKey.value = value;
}
</script>
