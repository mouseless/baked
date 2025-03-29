<template>
  <Button
    v-tooltip="{ value: 'Logout', showDelay: 300 }"
    severity="secondary"
    icon="pi pi-sign-out"
    text
    @click="clearToken"
  />
</template>
<script setup>
import { useToken } from "#imports";
import { Button } from "primevue";

const token = useToken();

function clearToken() {
  token.setCurrent(null);
}
</script>