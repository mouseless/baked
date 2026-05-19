<template>
  <slot />
</template>
<script setup>
import { useContext } from "#imports";

const context = useContext();

const validations = context.injectValidations();

const { validation } = defineProps({
  validation: { type: Object, default: null }
});

if(validation) {
  context.provideValidations({ ...validations, ...validation });
}
</script>
