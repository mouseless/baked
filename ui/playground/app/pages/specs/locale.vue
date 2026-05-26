<template>
  <UiSpec>
    <Message
      :schema="{ size: 'large' }"
      data="⬇️  Check if day name case is in current language ⬇️"
    />
    <div
      class="border-4 border-gray-500 rounded p-4 text-center"
      data-testid="test-datepicker"
    >
      <DatePicker inline />
    </div>
    <Message
      :schema="{ size: 'large' }"
      data="⬇️  Below you can see the supported locales ⬇️"
    />
    <div
      class="border-4 border-gray-500 rounded p-4 text-center"
      data-testid="test-locales"
    >
      {{ locales }}
    </div>
    <Message
      :schema="{ size: 'large' }"
      data="⬇️  Below you can see the current locale ⬇️"
    />
    <div
      class="border-4 border-gray-500 rounded p-4 text-center"
      data-testid="test-locale"
    >
      {{ locale.toUpperCase() }}
    </div>
  </UiSpec>
</template>
<script setup>
import { Message } from "#components";
import { DatePicker } from "primevue";
import { useLocalization } from "#imports";

const { locale, getLocales } = useLocalization();

const locales = getLocales().map(l => l.code.toUpperCase()).join(", ");
</script>