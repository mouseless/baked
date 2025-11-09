<template>
  <UiSpec
    title="Route Parameters"
  >
    <Message severity="info">
      <span class="text-xl">
        ⬆️  click the link below to navigate to a page with route parameter ⬆️
      </span>
    </Message>
    <Button
      as="a"
      label="/route-parameters-sample/42"
      href="/route-parameters-sample/42"
    />
  </UiSpec>
</template>
<script setup>
import { Button, Message } from "primevue";
</script>
