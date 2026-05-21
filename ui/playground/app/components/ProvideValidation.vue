<template>
  <slot />
</template>
<script setup>
import { useContext } from "#imports";

const context = useContext();

const { validation } = defineProps({
  validation: { type: Object, default: null }
});

if(validation) {
  context.provideValidation(validation);
}
</script>