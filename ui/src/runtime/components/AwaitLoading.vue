<template>
  <template v-if="loading">
    <slot name="loading">
      <Skeleton v-bind="skeleton" />
    </slot>
  </template>
  <template v-if="error">
    <slot
      name="error"
      :error
    >
      !!! {{ error }}
    </slot>
  </template>
  <slot v-else />
</template>
<script setup>
import { Skeleton } from "primevue";
import { useContext } from "#imports";

const { skeleton = {} } = defineProps({
  skeleton: { type: Object, default: () => ({}) }
});

const context = useContext();
const loading = context.injectLoading();
const error = context.injectError();
</script>