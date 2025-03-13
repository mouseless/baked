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
    <Panel header="Custom Handler" class="mt-4">
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
</script>