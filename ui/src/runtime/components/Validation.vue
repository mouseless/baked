<template>
  <template v-if="validationHandled || !validation">
    <slot />
  </template>
  <div
    v-else
    v-bind="$attrs"
    class="b-Validation flex flex-col gap-2"
  >
    <slot />
    <slot
      name="message"
      :validation
      :mutable-validation
    >
      <Bake
        v-if="message"
        :key="`${severity}:${icon}`"
        name="message"
        icon="pi pi-info-circle"
        :descriptor="{
          type: 'Message',
          schema: {
            severity,
            variant: 'simple',
            size: 'small',
            icon
          },
          data: {
            type: 'Inline',
            value: message || ''
          }
        }"
        class="ml-3"
      />
    </slot>
  </div>
</template>
<script setup>
import { computed } from "vue";
import { useContext } from "#imports";

const context = useContext();

defineOptions({
  inheritAttrs: false
});

const validationHandled = context.injectValidationHandled();
const validation = context.injectValidation();
const mutableValidation = context.injectMutableValidation();

context.provideValidationHandled(true);

const message = computed(() => {
  if(mutableValidation?.value.message) {
    return mutableValidation.value.message;
  }

  if(validation.value?.message && validation.value?.persist) {
    return validation.value.message;
  }

  return null;
});
const severity = computed(() => mutableValidation?.value.severity || validation?.value.severity);
const icon = computed(() => mutableValidation?.value.icon || validation?.value.icon);
</script>