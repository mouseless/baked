<template>
  <template v-if="!validation || validationHandled">
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
      :mutable-validation
    >
      <Message
        v-if="message"
        :schema="{
          severity: severity,
          variant: 'simple',
          size: 'small'
        }"
        :data="message || ''"
        class="ml-3"
      />
    </slot>
  </div>
</template>
<script setup>
import { computed } from "vue";
import { useContext } from "#imports";
import { Message } from "#components";

const context = useContext();

const validation = context.injectValidation();
const validationHandled = context.injectValidationHandled();
const mutableValidation = context.injectMutableValidation();

context.provideValidationHandled(true);

const message = computed(() => {
  if(mutableValidation?.value.message) {
    return mutableValidation.value.message;
  }

  if(validation?.value.message && validation.value.persist) {
    return validation.value.message;
  }

  return null;
});
const severity = computed(() => mutableValidation?.value.severity || validation?.value.severity);
</script>