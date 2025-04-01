<template>
  <UiSpec title="Auth" test-id="auth">
    <Message severity="warn">
      <span class="text-xl">
        This test requires authentication, make sure you are logged in if you
        see this page
      </span>
    </Message>
    <Divider />
    <Message severity="info">
      <span class="text-xl">
        ⬇️  Check if below button makes a successful request to backend ↗️
      </span>
    </Message>
    <div
      class="border-4 border-gray-500 rounded"
      data-testid="component"
    >
      <Button
        data-testid="request"
        type="button"
        label="Request with token"
        class="m-4"
        @click="async() => requestWithToken()"
      />
    </div>
    <Message severity="info">
      <span class="text-xl">
        ⬇️  Check if below button redirects you to login page with an error ↗️
      </span>
    </Message>
    <div
      class="border-4 border-gray-500 rounded"
      data-testid="component"
    >
      <Button
        data-testid="exception"
        type="button"
        label="Authentication Exception"
        class="m-4"
        @click="authenticationException"
      />
    </div>
    <Message severity="info">
      <span class="text-xl">
        ⬇️  Check if below button logs you out and redirects to login page ➡️
      </span>
    </Message>
    <div
      class="border-4 border-gray-500 rounded"
      data-testid="component"
    >
      <Button
        data-testid="logout"
        type="button"
        class="m-4"
        label="Logout"
        @click="logout"
      />
    </div>
  </UiSpec>
</template>
<script setup>
import { Button, Divider, Message } from "primevue";
import { useRuntimeConfig } from "#app";
import { createError, useToast, useToken } from "#imports";

const { public: { composables } } = useRuntimeConfig();
const token = useToken();
const toast = useToast();

function authenticationException() {
  throw createError({
    statusCode: 401,
    statusMessage: "Authentication Failed"
  });
}

async function requestWithToken(){
  const result = await $fetch("time-provider-samples/now", {
    baseURL: composables.useDataFetcher.baseURL,
    method: "GET"
  });

  toast.add({
    severity: "info",
    summary: "Server Time",
    detail: result,
    life: 3000
  });
}

function logout() {
  token.setCurrent(null);
}
</script>
