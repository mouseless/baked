<template>
  <template v-if="!validation">
    <slot />
  </template>
  <div
    v-else
    class="b-Validation flex flex-col gap-2"
  >
    <slot />
    <slot
      name="message"
      :validation
    >
      <Message
        v-show="validation.message && validation.persist"
        :severity="validation.severity"
        variant="simple"
        size="small"
        class="ml-2"
      >
        {{ validation.message || "" }}
      </Message>
    </slot>
  </div>
</template>
<script setup>
import { Message } from "primevue";
import { useContext } from "#imports";

const context = useContext();

const validation = context.injectValidation();
</script>