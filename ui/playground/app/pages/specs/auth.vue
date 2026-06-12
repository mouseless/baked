<template>
  <UiSpec test-id="auth">
    <Message
      :schema="{ size: 'large', severity: 'warn' }"
      data="This test requires authentication, make sure you are logged in if you see this page"
    />
    <Divider />
    <Message
      :schema="{ size: 'large' }"
      data="⬇️  Check if below button makes a successful request to backend ⬆️"
    />
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
    <Message
      :schema="{ size: 'large' }"
      data="⬇️  Check if below button redirects you to login page with an error ⬆️"
    />
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
    <Message
      :schema="{ size: 'large' }"
      data="⬇️  Check if below button logs you out and redirects to login page ➡️"
    />
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
import { Message } from "#components";
import { Button, Divider } from "primevue";
import { useRuntimeConfig } from "#app";
import { createError, useToast, useToken } from "#imports";

const { public: { apiBaseURL } } = useRuntimeConfig();
const token = useToken();
const toast = useToast();

function authenticationException() {
  throw createError({
    statusCode: 401,
    statusMessage: "Authentication Failed"
  });
}

async function requestWithToken() {
  const result = await $fetch("time-provider-samples/now", {
    baseURL: apiBaseURL,
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