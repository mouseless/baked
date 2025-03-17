<template>
  <UiSpec title="Error Handling" test-id="error-handling">
    <Panel header="Error Handlers" class="mt-4">
      <Button
        data-testid="alert"
        type="button"
        label="Alert"
        class="m-4"
        @click="alertError"
      />
      <Button
        data-testid="page"
        type="button"
        label="Page"
        class="m-4"
        @click="pageError"
      />
      <Button
        data-testid="redirect"
        type="button"
        label="Redirect"
        class="m-4"
        @click="redirectError"
      />
    </Panel>
  </UiSpec>
</template>
<script setup>
import { Button, Panel } from "primevue";
import { createError } from "#app";
import { FetchError} from "ofetch";

function alertError() {
  throw createError({
    statusCode: 400,
    statusMessage: "This error is displayed in toast!"
  });
}

function pageError() {
  throw createError({
    statusCode: 404,
    statusMessage: "This error displays full page!"
  });
}

function redirectError() {
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