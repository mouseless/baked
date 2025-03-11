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
import { createError, showError } from "#app";
import { useToast } from "#imports";

const toast = useToast();

function toastError(){
  toast.add({
    severity: "error",
    summary: "Summary",
    detail: "This is a test detail for a success toast message"
  });
}

function fullPageError(){
  const error = createError({
    statusCode: 404,
    statusMessage: "An error has occured!"
  });

  showError(error);
}
</script>