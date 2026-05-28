<template>
  <UiSpec>
    <Message
      :schema="{ size: 'large' }"
      data="⬇️  Change the language below ⬇️"
    />
    <div
      class="border-4 border-gray-500 rounded p-6 space-x-4"
      data-testid="component-block"
    >
      <LanguageSwitcher />
    </div>
    <Message
      :schema="{ size: 'large' }"
      data="⬇️  Below, we expect the text to change according to the language. ⬇️"
    />
    <div class="border-4 border-gray-500 rounded p-4 space-x-4">
      <span data-testid="text">{{ l("Spec: Test Text") }}</span>
    </div>
    <Message
      :schema="{ size: 'large' }"
      data="⬇️  Below, we expect the `Accept-Language` header to be send header according to the current language. ⬇️"
    />
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
import { Message } from "#components";
import { Button } from "primevue";
import { useRuntimeConfig } from "#app";
import { useLocalization } from "#imports";

const { localize: l } = useLocalization();
const { public: { apiBaseURL } } = useRuntimeConfig();

async function requestWithLanguageHeader() {
  await $fetch("time-provider-samples/now", {
    baseURL: apiBaseURL,
    method: "GET"
  }).catch(_ => { });
}
</script>