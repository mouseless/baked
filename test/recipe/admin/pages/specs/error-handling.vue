<template>
  <UiSpec title="Error Handling" test-id="error-handling">
    <Panel header="Default Handler" class="mt-4">
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
        data-testid="toast-options-from-fetch-error-data"
        type="button"
        label="Fetch error with data"
        class="m-4"
        @click="fetchErrorWithData"
      />
    </Panel>
  </UiSpec>
</template>
<script setup>
import { Button, Panel } from "primevue";
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

function fetchErrorWithData() {
  const error = new FetchError("Unauthorized");
  error.statusCode = 401;
  error.data = {
    "type": "https://baked.mouseless.codes/errors/authentication",
    "title": "Authentication",
    "status": 401,
    "detail": "Failed to authenticate with given credentials."
  };

  throw error;
}
</script>