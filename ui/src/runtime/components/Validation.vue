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
        :schema="{
          severity: validation.severity,
          variant: 'simple',
          size: 'small'
        }"
        v-show="validation.message && validation.persist"
        class="ml-2 mt-[-1rem]"
        :data="validation.message || ''"
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