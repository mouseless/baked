<template>
  <UiSpec
    :variants
    no-loading-variant
  >
    <Message
      :schema="{ size: 'large' }"
      data="⬇️  Use query parameters to test this page ⬇️"
    />
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
    <Message
      :schema="{ size: 'large', severity: 'success' }"
      data="💡 Refresh after clicking a button to reload below test variants 💡"
    />
  </UiSpec>
</template>
<script setup>
import { Message } from "#components";
import { Button } from "primevue";
import giveMe from "@utils/giveMe";

const variants = [
  {
    name: "Application",
    descriptor: giveMe.anExpected({
      testId: "application",
      data: giveMe.aRemoteData({
        path: "/cache-samples/application",
        query: giveMe.theQueryData(),
        attributes: { "client-cache": "application" }
      })
    })
  },
  {
    name: "User",
    descriptor: giveMe.anExpected({
      testId: "user",
      data: giveMe.aRemoteData({
        path: "/cache-samples/scoped",
        query: giveMe.theQueryData(),
        attributes: { "client-cache": "user" }
      })
    })
  }
];
</script>