<template>
  <template v-if="!validation">
    <slot />
  </template>
  <div
    v-else
    class="b-Validation flex flex-col gap-2"
  >
    <slot />
    <slot name="message">
      <Message
        v-if="validation.message && validation.persist"
        :schema="{
          severity: validation.severity,
          variant: 'simple',
          size: 'small'
        }"
        :data="validation.message || ''"
        class="ml-3"
      />
    </slot>
  </div>
</template>
<script setup>
import { useContext } from "#imports";
import { Message } from "#components";

const context = useContext();

const validation = context.injectValidation();
</script>