<template>
  <slot />
</template>
<script setup>
import { useContext } from "#imports";

const context = useContext();

const parentContext = context.injectParentContext();

const { validation } = defineProps({
  validation: { type: Object }
});

if(validation) {
  context.provideParentContext({ ...parentContext, ...validation });
}
</script>
