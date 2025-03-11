<template>
  <UiSpec title="Locale">
    <Message severity="info">
      <span class="text-xl">
        ⬇️  Check if day name case is iNVERTED ⬇️
      </span>
    </Message>
    <div class="border-4 border-gray-500 rounded p-4" data-testid="test">
      <DatePicker inline />
    </div>
  </UiSpec>
</template>
<script setup>
import { DatePicker, Message } from "primevue";
</script>
