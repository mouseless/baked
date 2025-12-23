<template>
  <slot />
</template>
<script setup>
import { useContext } from "#imports";

const context = useContext();

const { data, dataKey } = defineProps({
  data: { type: null, required: true },
  dataKey: { type: String, required: true }
});

const parentContext = context.injectParentContext();

context.provideParentContext({ ...parentContext, [dataKey]: data });
</script>
