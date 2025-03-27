<template>
  <UiSpec title="Auth" test-id="auth">
    <Panel class="mt-4">
      <div>
        This content requires authorization
      </div>
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
import { createError, useToken } from "#imports";

const token = useToken();

function authenticationException() {
  throw createError({
    statusCode: 401,
    statusMessage: "Authentication Failed"
  });
}

function logout() {
  token.setCurrent(null);
}
</script>