<template>
  <UiSpec title="Language Switcher">
    <Message severity="info">
      <span class="text-xl">
        ⬇️  Change the language below ⬇️
      </span>
    </Message>
    <div
      class="border-4 border-gray-500 rounded p-6 space-x-4"
      data-testid="component-block"
    >
      <LanguageSwitcher />
    </div>
    <Message severity="info">
      <span class="text-xl">
        ⬇️  Below, we expect the text to change according to the language. ⬇️
      </span>
    </Message>
    <div class="border-4 border-gray-500 rounded p-4 space-x-4">
      <span data-testid="text">{{ l("Spec: Test Text") }}</span>
    </div>
    <Message severity="info">
      <span class="text-xl">
        ⬇️  Below, we expect the `Accept-Language` header to be send header according to the current language. ⬇️
      </span>
    </Message>
    <div
      class="border-4 border-gray-500 rounded"
      data-testid="component"
    >
      <Button
        data-testid="requestWithLanguageHeader"
        type="button"
        class="m-4"
        label="Request With Language Header"
        @click="requestWithLanguageHeader"
      />
    </div>
  </UiSpec>
</template>
<script setup>
import { Button, Message } from "primevue";
import { useRuntimeConfig } from "#app";
import { useLocalization } from "#imports";

const { localize: l } = useLocalization();
const { public: { baseURL } } = useRuntimeConfig();

async function requestWithLanguageHeader() {
  await $fetch("time-provider-samples/now", {
    baseURL: baseURL,
    method: "GET"
  }).catch(_ => { });
}
</script>
