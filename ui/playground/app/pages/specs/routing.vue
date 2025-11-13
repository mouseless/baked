<template>
  <UiSpec
    title="Route Parameters"
  >
    <Message severity="info">
      <span class="text-xl">
        ⬆️  click the link below to navigate to a page with route parameter ⬆️
      </span>
    </Message>
    <div class="flex gap-4 flex-col">
      <Button
        as="routerLink"
        label="/page/with/route/pageWithRoute"
        to="/page/with/route/pageWithRoute"
      />
      <Button
        as="routerLink"
        label="/parent/a6db0515-a885-4cf6-8343-c2c4a0510392"
        to="/parent/a6db0515-a885-4cf6-8343-c2c4a0510392"
      />
      <Button
        as="routerLink"
        label="/parent/1/children/2"
        to="/parent/1/children/2"
      />
    </div>
  </UiSpec>
</template>
<script setup>
import { Button, Message } from "primevue";
</script>
