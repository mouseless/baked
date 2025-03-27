<template>
  <UiSpec title="Auth" test-id="auth">
    <Panel class="mt-4">
      <div>
        This content requires authorization
      </div>
      <Button
        data-testid="request"
        type="button"
        label="Request with token"
        class="m-4"
        @click="async() => requestWithToken()"
      />
      <Button
        data-testid="exception"
        type="button"
        label="Authentication Exception"
        class="m-4"
        @click="authenticationException"
      />
      <Button
        data-testid="logout"
        type="button"
        class="m-4"
        label="Logout"
        @click="logout"
      />
    </Panel>
  </UiSpec>
</template>
<script setup>
import { Button, Panel } from "primevue";
import { createError, useToast, useToken } from "#imports";

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
    baseURL: "http://localhost:5151",
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