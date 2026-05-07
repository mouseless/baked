<template>
  <Select
    v-if="isXs"
    v-model="model"
    v-bind="$attrs"
    class="
      mr-2 shadow-none rounded-md
      bg-slate-100 border-slate-100
      dark:bg-zinc-950 dark:border-zinc-950"
  />
</template>
<script setup>
import { useBreakpoints } from "#imports";
import { Select } from "#components";

const { isXs } = useBreakpoints();

const model = defineModel({ type: null, required: true });
</script>
<style>
.b-component--PageSize {
  .p-select {
    @apply border-none
  }
  .p-select-label {
    font-size: inherit;
  }
}
</style>