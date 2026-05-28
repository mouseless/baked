<template>
  <UiSpec>
    <Message
      :schema="{ size: 'large' }"
      data="⬇️  Check if there is only `VISIBLE` below ⬇️"
    />
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
import { Message } from "#components";
</script>