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

const providedData = { ...parentContext };
providedData[dataKey] = data;
context.provideParentContext(providedData);
</script>