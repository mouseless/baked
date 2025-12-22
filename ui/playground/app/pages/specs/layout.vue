<template>
  <UiSpec
    title="Layout"
    test-id="layout"
  >
    <Message severity="warn">
      <span class="text-xl">
        This page should be displayed with `Default` layout
      </span>
    </Message>
    <Divider />
    <Message severity="info">
      <span class="text-xl">
        ⬇️  Check if below button displays `Login` page with `Modal` layout ⬆️
      </span>
    </Message>
    <div
      class="border-4 border-gray-500 rounded"
      data-testid="component"
    >
      <Button
        data-testid="login"
        type="button"
        label="Page with Modal Layout"
        class="m-4"
        @click="$router.push(loginPath)"
      />
    </div>
  </UiSpec>
</template>
<script setup>
import { Button, Divider, Message } from "primevue";
import { useLoginRedirect } from "#imports";

const loginRedirect = useLoginRedirect();
const loginPath = loginRedirect.computeSync();
</script>
