<template>
  <UiSpec title="Error Handling" test-id="error-handling">
    <Button
      data-testid="toast-error"
      type="button"
      label="Show toast error"
      class="m-4"
      @click="toastError"
    />
    <Button
      data-testid="full-page-error"
      type="button"
      label="Show full page error"
      class="m-4"
      @click="fullPageError"
    />
  </UiSpec>
</template>
<script setup>
import { Button } from "primevue";
import { createError } from "#app";

function toastError(){
  throw createError({
    statusCode: 400,
    statusMessage: "This error is displayed in toast!"
  });
}

function fullPageError(){
  throw createError({
    statusCode: 404,
    statusMessage: "This error displays full page!"
  });
}
</script>