<template>
  <UiSpec title="Auth" test-id="auth">
    <Panel class="mt-4">
      <div>
        This content requires authorization
      </div>
      <Logout />
    </Panel>
  </UiSpec>
</template>
<script setup>
import { Panel } from "primevue";
import { Logout } from "#components";
</script>