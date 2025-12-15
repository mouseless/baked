<template>
  <UiSpec title="Query Bound Inputs">
    <Message severity="info">
      <span class="text-xl">
        ⬆️  Check if values sync with query string above ⬆️
      </span>
    </Message>
    <div
      class="border-4 border-gray-500 rounded p-4 space-x-4"
      data-testid="component"
    >
      <QueryBoundInputs
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
        ⬇️  Check if unique key changes when any parameter changes ⬇️
      </span>
    </Message>
    <div class="border-4 border-gray-500 rounded p-4">
      <span class="text-gray-500">unique-key=</span>
      <span data-testid="unique-key">{{ uniqueKey }}</span>
    </div>
    <Message severity="info">
      <span class="text-xl">
        ⬇️  Click and check if resets all inputs except required with default ⬆️
      </span>
    </Message>
    <div class="border-4 border-gray-500 rounded p-4 flex gap-4">
      <Button
        as="router-link"
        label="RESET"
        to="/specs/query-bound-inputs"
        data-testid="reset"
      />
      <Bake
        name="reactor"
        :descriptor="reactor"
        class="border border-green-500 rounded p-2"
      />
    </div>
  </UiSpec>
</template>
<script setup>
import { ref } from "vue";
import { Button, Message } from "primevue";
import giveMe from "@utils/giveMe";

const ready = ref();
const uniqueKey = ref();

const inputs = [
  giveMe.anInput({
    name: "requiredWithDefault",
    component: giveMe.anInputText({ testId: "required-with-default" }),
    required: true,
    defaultValue: "default value"
  }),
  giveMe.anInput({
    name: "requiredWithDefaultSelfManaged",
    required: true,
    defaultSelfManaged: true,
    component: giveMe.anInputText({ testId: "required-with-default-self-managed", defaultValue: "default" })
  }),
  giveMe.anInput({
    name: "required",
    component: giveMe.anInputText({ testId: "required" }),
    required: true
  }),
  giveMe.anInput({
    name: "optional",
    component: giveMe.anInputText({
      testId: "optional",
      action: giveMe.aPublishAction({ pageContextKey: "optional" })
    })
  }),
  giveMe.anInput({
    name: "num-required",
    component: giveMe.anInputNumber({ testId: "num-required" }),
    required: true
  })
];

const reactor = giveMe.anExpected({
  testId: "reactor",
  value: "Reacting...",
  reactions: {
    show: giveMe.aTrigger({
      when: "optional",
      constraint: giveMe.aConstraint({ is: "react" })
    })
  }
});

function onReady(value) {
  ready.value = value;
}

function onChanged(value) {
  uniqueKey.value = value;
}
</script>
