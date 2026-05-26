<template>
  <UiSpec>
    <Message
      :schema="{size: 'large'}"
      data="⬆️  click the link below to navigate to a page with route parameter ⬆️"
    />
    <div class="flex gap-4 flex-col">
      <Button
        as="routerLink"
        label="/page/with/route/pageWithRoute"
        to="/page/with/route/pageWithRoute"
      />
      <Button
        as="routerLink"
        label="/first/1"
        to="/first/1"
      />
      <Button
        as="routerLink"
        label="/first/1/second/2"
        to="/first/1/second/2"
      />
    </div>
  </UiSpec>
</template>
<script setup>
import { Message } from "#components";
import { Button } from "primevue";
</script>