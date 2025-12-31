<template>
  <UiSpec title="Inputs - Query Bound">
    <Message severity="info">
      <span class="text-xl">
        ⬆️  Check if values sync with query string above ⬆️
      </span>
    </Message>
    <div
      class="border-4 border-gray-500 rounded p-4 space-x-4"
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
        to="/specs/inputs--query-bound"
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
    required: true,
    defaultValue: "default value",
    queryBound: true,
    component: giveMe.anExpectedInput({ testId: "required-with-default" })
  }),
  giveMe.anInput({
    name: "requiredWithDefaultSelfManaged",
    required: true,
    defaultSelfManaged: true,
    queryBound: true,
    component: giveMe.anExpectedInput({ testId: "required-with-default-self-managed", defaultValue: "default" })
  }),
  giveMe.anInput({
    name: "required",
    required: true,
    queryBound: true,
    component: giveMe.anExpectedInput({ testId: "required" })
  }),
  giveMe.anInput({
    name: "required-number",
    required: true,
    queryBound: true,
    component: giveMe.anExpectedInput({ testId: "required-number", number: true })
  }),
  giveMe.anInput({
    name: "optional",
    queryBound: true,
    component: giveMe.anExpectedInput({
      testId: "optional",
      action: giveMe.aPublishAction({ pageContextKey: "optional" })
    })
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

function onChanged(event) {
  uniqueKey.value = event.uniqueKey;
}
</script>
