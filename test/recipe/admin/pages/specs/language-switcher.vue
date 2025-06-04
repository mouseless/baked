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
      <span data-testid="text">{{ l("Test Text") }}</span>
    </div>
	</UiSpec>
</template>
<script setup>
import { useLocalization } from "#imports";
import { Message } from "primevue";

const { localize: l } = useLocalization();
</script>
