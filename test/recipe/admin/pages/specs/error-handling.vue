<template>
  <UiSpec title="Error Handling" test-id="error-handling">
    <Button
      data-testid="toast-error"
      type="button"
      label="Toast error"
      class="m-4"
      @click="toastError"
    />
    <Button
      data-testid="full-page-error"
      type="button"
      label="Full page error"
      class="m-4"
      @click="fullPageError"
    />
    <Button
      data-testid="custom-handler-toast"
      type="button"
      label="Custom Toast Error"
      class="m-4"
      @click="customHandlerToast"
    />
    <Button
      data-testid="custom-handler-full-page"
      type="button"
      label="Custom full page Error"
      class="m-4"
      @click="customHandlerFullPage"
    />
    <Button
      type="button"
      label="Fetch error from backend"
      class="m-4"
      @click="async () => fetchErrorFromBackend()"
    />
  </UiSpec>
</template>
<script setup>
import { Button } from "primevue";
import { createError } from "#app";
import { FetchError} from "ofetch";

function toastError() {
  throw createError({
    statusCode: 400,
    statusMessage: "This error is displayed in toast!"
  });
}

function fullPageError() {
  throw createError({
    statusCode: 404,
    statusMessage: "This error displays full page!"
  });
}

function customHandlerToast() {
  const error = new FetchError("Bad request");
  error.statusCode = 400;

  throw error;
}

function customHandlerFullPage() {
  const error = new FetchError("Unauthorized");
  error.statusCode = 403;

  throw error;
}

async function fetchErrorFromBackend() {
  await $fetch("exception-samples/throw?handled=true", {
    method: "POST",
    baseURL: "https://localhost:7254"
  });
}
</script>