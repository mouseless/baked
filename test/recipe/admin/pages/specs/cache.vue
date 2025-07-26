<template>
  <UiSpec
    title="Cache"
    :variants="variants"
    :no-loading-variant="true"
  >
    <Message severity="info">
      <span class="text-xl">
        ⬇️  Use query parameters to test this page ⬇️
      </span>
    </Message>
    <div class="border-4 border-gray-500 rounded p-4 space-x-4">
      <Button
        type="button"
        label="value_a"
        as="router-link"
        to="/specs/cache?parameter=value_a"
      />
      <Button
        type="button"
        label="value_b"
        as="router-link"
        to="/specs/cache?parameter=value_b"
      />
    </div>
  </UiSpec>
</template>
<script setup>
import { Button, Message } from "primevue";
import giveMe from "~/utils/giveMe";

const variants = [
  {
    name: "Application",
    descriptor: giveMe.anExpected({
      testId: "application",
      data: {
        type: "Remote",
        path: "/cache-samples/application",
        query: {
          "type": "Computed",
          "composable": "useQuery",
          "args": []
        },
        options: { "client-cache": "application" }
      }
    })
  },
  {
    name: "User",
    descriptor: giveMe.anExpected({
      testId: "user",
      data: {
        type: "Remote",
        path: "/cache-samples/scoped",
        query: {
          "type": "Computed",
          "composable": "useQuery",
          "args": []
        },
        options: { "client-cache": "user" }
      }
    })
  }
];
</script>
