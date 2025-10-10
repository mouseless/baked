<template>
  <UiSpec title="Custom CSS">
    <Message severity="info">
      <span class="text-xl">
        ⬇️  Check if there is only `VISIBLE` below ⬇️
      </span>
    </Message>
    <div
      class="border-4 border-gray-500 rounded p-4"
      data-testid="test"
    >
      <div class="hidden custom-css-visible">VISIBLE</div>
      <div class="visible custom-css-hidden">HIDDEN</div>
    </div>
  </UiSpec>
</template>
<script setup>
import { Message } from "primevue";
</script>
