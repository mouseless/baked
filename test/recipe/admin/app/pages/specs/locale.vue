<template>
  <UiSpec title="Locale">
    <Message severity="info">
      <span class="text-xl">
        ⬇️  Check if day name case is in current language ⬇️
      </span>
    </Message>
    <div class="border-4 border-gray-500 rounded p-4 text-center" data-testid="test-datepicker">
      <DatePicker inline />
    </div>
    <Message severity="info">
      <span class="text-xl">
        ⬇️  Below you can see the supported locales ⬇️
      </span>
    </Message>
    <div class="border-4 border-gray-500 rounded p-4 text-center" data-testid="test-locales">
      {{ locales }}
    </div>
    <Message severity="info">
      <span class="text-xl">
        ⬇️  Below you can see the current locale ⬇️
      </span>
    </Message>
    <div class="border-4 border-gray-500 rounded p-4 text-center" data-testid="test-locale">
      {{ locale.toUpperCase() }}
    </div>
  </UiSpec>
</template>
<script setup>
import { DatePicker, Message } from "primevue";
import { useLocalization } from "#imports";

const { locale, getLocales } = useLocalization();

const locales = getLocales().map(l => l.code.toUpperCase()).join(", ");
</script>
